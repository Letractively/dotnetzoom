'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'


Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web

Namespace DotNetZoom

    Public Class PortalSecurity

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' The FilterFlag enum determines which filters are applied by the InputFilter
        ''' function.  The Flags attribute allows the user to include multiple 
        ''' enumerated values in a single variable by OR'ing the individual values
        ''' together.
        ''' </summary>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created  Bug #000120, #000121
        ''' </history>
        '''-----------------------------------------------------------------------------
        <FlagsAttribute()> _
        Enum FilterFlag
            MultiLine = 1
            NoMarkup = 2
            NoScripting = 4
            NoSQL=8
        End Enum

        Public Function UserLogin(ByVal Username As String, ByVal Password As String, ByVal PortalID As Integer) As Integer

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim intUserId As Integer = -1

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Username, Encrypt(portalSettings.GetHostSettings("EncryptionKey"), Password), PortalID})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            If result.Read Then
                If Not IsDBNull(result("UserId")) Then
                    intUserId = Int32.Parse(result("UserId"))
                End If
            End If
            result.Close()

            Return intUserId

        End Function


        Public Shared Function IsSuperUser() As Boolean

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			If HttpContext.Current.Request.IsAuthenticated then
            If HttpContext.Current.User.Identity.Name = _portalSettings.SuperUserId.ToString Then
                Return True
            End If
			End If
			Return False

        End Function
		
		
		
        Public Shared Function IsInRole(ByVal role As String) As Boolean

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If HttpContext.Current.User.Identity.Name = _portalSettings.SuperUserId.ToString Then
                Return True
            Else
                Return HttpContext.Current.User.IsInRole(role)
            End If

        End Function


        Public Shared Function IsInRoles(ByVal roles As String) As Boolean

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim context As HttpContext = HttpContext.Current

            Dim role As String
            For Each role In roles.Split(New Char() {";"c})
                If role <> "" And Not role Is Nothing And _
                    ((context.Request.IsAuthenticated = False And role = glbRoleUnauthUser) Or _
                    role = glbRoleAllUsers Or _
                    context.User.IsInRole(role) = True Or _
                    context.User.Identity.Name = _portalSettings.SuperUserId.ToString) Then
                    Return True
                End If
            Next role

            Return False

        End Function

        Public Shared Function HasEditPermissions(ByVal ModuleId As Integer) As Boolean

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim result As SqlDataReader = GetAuthRoles(_portalSettings.PortalId, ModuleId)

            If result.Read() Then
                If IsInRoles(result("AuthorizedRoles").ToString) = False Or IsInRoles(result("AuthorizedEditRoles").ToString) = False Then
                    HasEditPermissions = False
                Else
                    HasEditPermissions = True
                End If
            Else
                HasEditPermissions = False
            End If
            result.Close()

        End Function

        Public Shared Function GetAuthRoles(ByVal PortalId As Integer, ByVal ModuleId As Integer) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, ModuleId})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return result

        End Function

        Public Function EncryptURL(ByVal strKey As String, ByVal strData As String) As String
            Return glbPath + EncryptRijndael(strKey, strData) + "/controls/crypto.ashx"
        End Function

        Public Function EncryptRijndael(ByVal strKey As String, ByVal strData As String) As String
            Dim sym As New Symmetric(Symmetric.Provider.Rijndael)
            Dim key As New CryptoData(strKey)
            Dim encryptedData As CryptoData
            encryptedData = sym.Encrypt(New CryptoData(strData), key)
            'Return encryptedData.ToBase64
            Return encryptedData.ToHex
        End Function
        Public Function DecryptRijndael(ByVal strKey As String, ByVal strData As String) As String
            Dim sym As New Symmetric(Symmetric.Provider.Rijndael)
            Dim key As New CryptoData(strKey)
            Dim encryptedData As New CryptoData
            'encryptedData.Base64 = strData
            encryptedData.Hex = strData
            Dim decryptedData As CryptoData
            decryptedData = sym.Decrypt(encryptedData, key)
            Return decryptedData.ToString
        End Function

        Public Function Encrypt(ByVal strKey As String, ByVal strData As String) As String

            Dim strValue As String = ""

            If strKey <> "" Then
                ' convert key to 16 characters for simplicity
                Select Case Len(strKey)
                    Case Is < 16
                        strKey = strKey & Left("XXXXXXXXXXXXXXXX", 16 - Len(strKey))
                    Case Is > 16
                        strKey = Left(strKey, 16)
                End Select

                ' create encryption keys
                Dim byteKey() As Byte = Encoding.UTF8.GetBytes(Left(strKey, 8))
                Dim byteVector() As Byte = Encoding.UTF8.GetBytes(Right(strKey, 8))

                ' convert data to byte array
                Dim byteData() As Byte = Encoding.UTF8.GetBytes(strData)

                ' encrypt 
                Dim objDES As New DESCryptoServiceProvider
                Dim objMemoryStream As New MemoryStream
                Dim objCryptoStream As New CryptoStream(objMemoryStream, objDES.CreateEncryptor(byteKey, byteVector), CryptoStreamMode.Write)
                objCryptoStream.Write(byteData, 0, byteData.Length)
                objCryptoStream.FlushFinalBlock()

                ' convert to string and Base64 encode
                strValue = Convert.ToBase64String(objMemoryStream.ToArray())
            Else
                strValue = strData
            End If

            Return strValue

        End Function

        Public Function Decrypt(ByVal strKey As String, ByVal strData As String) As String

            Dim strValue As String = ""

            If strKey <> "" Then
                ' convert key to 16 characters for simplicity
                Select Case Len(strKey)
                    Case Is < 16
                        strKey = strKey & Left("XXXXXXXXXXXXXXXX", 16 - Len(strKey))
                    Case Is > 16
                        strKey = Left(strKey, 16)
                End Select

                ' create encryption keys
                Dim byteKey() As Byte = Encoding.UTF8.GetBytes(Left(strKey, 8))
                Dim byteVector() As Byte = Encoding.UTF8.GetBytes(Right(strKey, 8))

                ' convert data to byte array and Base64 decode
                Dim byteData(strData.Length) As Byte
                Try
                    byteData = Convert.FromBase64String(strData)
                Catch ' invalid length
                    strValue = strData + " invalid length"
                End Try

                If strValue = "" Then
                    Try
                        ' decrypt
                        Dim objDES As New DESCryptoServiceProvider
                        Dim objMemoryStream As New MemoryStream
                        Dim objCryptoStream As New CryptoStream(objMemoryStream, objDES.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Write)
                        objCryptoStream.Write(byteData, 0, byteData.Length)
                        objCryptoStream.FlushFinalBlock()

                        ' convert to string
                        Dim objEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
                        strValue = objEncoding.GetString(objMemoryStream.ToArray())
                    Catch ' decryption error
                        strValue = ""
                    End Try
                End If
            Else
                strValue = strData
            End If

            Return strValue

        End Function

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' This function applies security filtering to the UserInput string.
        ''' </summary>
        ''' <param name="UserInput">This is the string to be filtered</param>
        ''' <param name="FilterType">Flags which designate the filters to be applied</param>
        ''' <returns>Filtered UserInput</returns>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created Bug #000120, #000121
        ''' </history>
        '''-----------------------------------------------------------------------------
        Public Function InputFilter(ByVal UserInput As String, ByVal FilterType As FilterFlag) As String
            Dim TempInput As String = UserInput

            If (FilterType And FilterFlag.NoSQL) = FilterFlag.NoSQL Then
                TempInput = FormatRemoveSQL(TempInput)
            Else
                If (FilterType And FilterFlag.NoMarkup) = FilterFlag.NoMarkup Then
                    If IncludesMarkup(TempInput) Then
                        TempInput = HttpUtility.HtmlEncode(TempInput)
                    End If
                ElseIf (FilterType And FilterFlag.NoScripting) = FilterFlag.NoScripting Then
                    TempInput = FormatDisableScripting(TempInput)
                End If

                If (FilterType And FilterFlag.MultiLine) = FilterFlag.MultiLine Then
                    TempInput = FormatMultiLine(TempInput)
                End If
            End If

            Return TempInput
        End Function

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' This filter removes CrLf characters and inserts <br>
        ''' </summary>
        ''' <param name="strInput">This is the string to be filtered</param>
        ''' <returns>Filtered UserInput</returns>
        ''' <remarks>
        ''' This is a private function that is used internally by the InputFilter function
        ''' </remarks>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created Bug #000120
        ''' </history>
        '''-----------------------------------------------------------------------------
        Private Function FormatMultiLine(ByVal strInput As String) As String
            Dim TempInput As String = strInput.Replace(ControlChars.Cr + ControlChars.Lf, "<br>")
            Return TempInput.Replace(ControlChars.Cr, "<br>")
        End Function 'FormatMultiLine

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' This function uses Regex search strings to remove HTML tags which are 
        ''' targeted in Cross-site scripting (XSS) attacks.  This function will evolve
        ''' to provide more robust checking as additional holes are found.
        ''' </summary>
        ''' <param name="strInput">This is the string to be filtered</param>
        ''' <returns>Filtered UserInput</returns>
        ''' <remarks>
        ''' This is a private function that is used internally by the InputFilter function
        ''' </remarks>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created Bug #000120
        ''' </history>
        '''-----------------------------------------------------------------------------
        Private Function FormatDisableScripting(ByVal strInput As String) As String
            Dim TempInput As String = strInput

            Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
            Dim strReplacement As String = " "
            Dim strPattern As String = "<script[^>]*>.*?</script[^><]*>"

            TempInput = Regex.Replace(TempInput, "<script[^>]*>.*?</script[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<input[^>]*>.*?</input[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<object[^>]*>.*?</object[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<applet[^>]*>.*?</applet[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<form[^>]*>.*?</form[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<option[^>]*>.*?</option[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<select[^>]*>.*?</select[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<iframe[^>]*>.*?</iframe[^><]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "<form[^>]*>", strReplacement, options)
            TempInput = Regex.Replace(TempInput, "</form[^><]*>", strReplacement, options)
            Return TempInput
        End Function 'FormatDisableScripting

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' This function verifies raw SQL statements to prevent SQL injection attacks 
        ''' and replaces a similar function (PreventSQLInjection) from the Globals.vb module
        ''' </summary>
        ''' <param name="strSQL">This is the string to be filtered</param>
        ''' <returns>Filtered UserInput</returns>
        ''' <remarks>
        ''' This is a private function that is used internally by the InputFilter function
        ''' </remarks>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created Bug #000121
        ''' </history>
        '''-----------------------------------------------------------------------------
        Private Function FormatRemoveSQL(ByVal strSQL As String) As String

            Dim strCleanSQL As String = strSQL

            Dim BadCommands As Array = Split(";,--,create,drop,select,insert,delete,update,union,sp_,xp_", ",")

            ' strip any dangerous SQL commands
            Dim intCommand As Integer
            For intCommand = 0 To BadCommands.Length - 1
                If Not Trim(strCleanSQL) = "" Then
                    strCleanSQL = Regex.Replace(strCleanSQL, BadCommands(intCommand), " ", RegexOptions.IgnoreCase)
                End If
            Next

            ' convert any single quotes
            strCleanSQL = Replace(strCleanSQL, "'", "''")

            Return strCleanSQL

        End Function

        '''-----------------------------------------------------------------------------
        ''' <summary>
        ''' This function determines if the Input string contains any markup.
        ''' </summary>
        ''' <param name="strInput">This is the string to be checked</param>
        ''' <returns>True if string contains Markup tag(s)</returns>
        ''' <remarks>
        ''' This is a private function that is used internally by the InputFilter function
        ''' </remarks>
        ''' <history>
        ''' 	[Joe Brinkman] 	8/15/2003	Created Bug #000120
        ''' </history>
        '''-----------------------------------------------------------------------------
        Private Function IncludesMarkup(ByVal strInput As String) As Boolean
            Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
            Dim strPattern As String = "<[^<>]*>"
            Return Regex.IsMatch(strInput, strPattern, options)
        End Function

    End Class

    Public Class Symmetric

        Private Const _DefaultIntializationVector As String = "%1Az=-@qT"
        Private Const _BufferSize As Integer = 2048

        Public Enum Provider
            ''' <summary>
            ''' The Data Encryption Standard provider supports a 64 bit key only
            ''' </summary>
            DES
            ''' <summary>
            ''' The Rivest Cipher 2 provider supports keys ranging from 40 to 128 bits, default is 128 bits
            ''' </summary>
            RC2
            ''' <summary>
            ''' The Rijndael (also known as AES) provider supports keys of 128, 192, or 256 bits with a default of 256 bits
            ''' </summary>
            Rijndael
            ''' <summary>
            ''' The TripleDES provider (also known as 3DES) supports keys of 128 or 192 bits with a default of 192 bits
            ''' </summary>
            TripleDES
        End Enum

        Private _data As CryptoData
        Private _key As CryptoData
        Private _iv As CryptoData
        Private _crypto As SymmetricAlgorithm
        Private _EncryptedBytes As Byte()
        Private _UseDefaultInitializationVector As Boolean

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Instantiates a new symmetric encryption object using the specified provider.
        ''' </summary>
        Public Sub New(ByVal provider As Provider, Optional ByVal useDefaultInitializationVector As Boolean = True)
            Select Case provider
                Case provider.DES
                    _crypto = New DESCryptoServiceProvider
                Case provider.RC2
                    _crypto = New RC2CryptoServiceProvider
                Case provider.Rijndael
                    _crypto = New RijndaelManaged
                Case provider.TripleDES
                    _crypto = New TripleDESCryptoServiceProvider
            End Select

            '-- make sure key and IV are always set, no matter what
            Me.Key = RandomKey()
            If useDefaultInitializationVector Then
                Me.IntializationVector = New CryptoData(_DefaultIntializationVector)
            Else
                Me.IntializationVector = RandomInitializationVector()
            End If
        End Sub

        ''' <summary>
        ''' Key size in bytes. We use the default key size for any given provider; if you 
        ''' want to force a specific key size, set this property
        ''' </summary>
        Public Property KeySizeBytes() As Integer
            Get
                Return _crypto.KeySize \ 8
            End Get
            Set(ByVal Value As Integer)
                _crypto.KeySize = Value * 8
                _key.MaxBytes = Value
            End Set
        End Property

        ''' <summary>
        ''' Key size in bits. We use the default key size for any given provider; if you 
        ''' want to force a specific key size, set this property
        ''' </summary>
        Public Property KeySizeBits() As Integer
            Get
                Return _crypto.KeySize
            End Get
            Set(ByVal Value As Integer)
                _crypto.KeySize = Value
                _key.MaxBits = Value
            End Set
        End Property

        ''' <summary>
        ''' The key used to encrypt/decrypt data
        ''' </summary>
        Public Property Key() As CryptoData
            Get
                Return _key
            End Get
            Set(ByVal Value As CryptoData)
                _key = Value
                _key.MaxBytes = _crypto.LegalKeySizes(0).MaxSize \ 8
                _key.MinBytes = _crypto.LegalKeySizes(0).MinSize \ 8
                _key.StepBytes = _crypto.LegalKeySizes(0).SkipSize \ 8
            End Set
        End Property

        ''' <summary>
        ''' Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
        ''' the value derived from the previous block; the first data block has no previous data block
        ''' to use, so it needs an InitializationVector to feed the first block
        ''' </summary>
        Public Property IntializationVector() As CryptoData
            Get
                Return _iv
            End Get
            Set(ByVal Value As CryptoData)
                _iv = Value
                _iv.MaxBytes = _crypto.BlockSize \ 8
                _iv.MinBytes = _crypto.BlockSize \ 8
            End Set
        End Property

        ''' <summary>
        ''' generates a random Initialization Vector, if one was not provided
        ''' </summary>
        Public Function RandomInitializationVector() As CryptoData
            _crypto.GenerateIV()
            Dim d As New CryptoData(_crypto.IV)
            Return d
        End Function

        ''' <summary>
        ''' generates a random Key, if one was not provided
        ''' </summary>
        Public Function RandomKey() As CryptoData
            _crypto.GenerateKey()
            Dim d As New CryptoData(_crypto.Key)
            Return d
        End Function

        ''' <summary>
        ''' Ensures that _crypto object has valid Key and IV
        ''' prior to any attempt to encrypt/decrypt anything
        ''' </summary>
        Private Sub ValidateKeyAndIv(ByVal isEncrypting As Boolean)
            If _key.IsEmpty Then
                If isEncrypting Then
                    _key = RandomKey()
                Else
                    ' Throw New CryptographicException("No key was provided for the decryption operation!")
                End If
            End If
            If _iv.IsEmpty Then
                If isEncrypting Then
                    _iv = RandomInitializationVector()
                Else
                    ' Throw New CryptographicException("No initialization vector was provided for the decryption operation!")
                End If
            End If
            _crypto.Key = _key.Bytes
            _crypto.IV = _iv.Bytes
        End Sub

        ''' <summary>
        ''' Encrypts the specified Data using provided key
        ''' </summary>
        Public Function Encrypt(ByVal d As CryptoData, ByVal key As CryptoData) As CryptoData
            Me.Key = key
            Return Encrypt(d)
        End Function

        ''' <summary>
        ''' Encrypts the specified Data using preset key and preset initialization vector
        ''' </summary>
        Public Function Encrypt(ByVal d As CryptoData) As CryptoData
            Dim ms As New IO.MemoryStream

            ValidateKeyAndIv(True)

            Dim cs As New CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write)
            cs.Write(d.Bytes, 0, d.Bytes.Length)
            cs.Close()
            ms.Close()

            Return New CryptoData(ms.ToArray)
        End Function

        ''' <summary>
        ''' Encrypts the stream to memory using provided key and provided initialization vector
        ''' </summary>
        Public Function Encrypt(ByVal s As Stream, ByVal key As CryptoData, ByVal iv As CryptoData) As CryptoData
            Me.IntializationVector = iv
            Me.Key = key
            Return Encrypt(s)
        End Function

        ''' <summary>
        ''' Encrypts the stream to memory using specified key
        ''' </summary>
        Public Function Encrypt(ByVal s As Stream, ByVal key As CryptoData) As CryptoData
            Me.Key = key
            Return Encrypt(s)
        End Function

        ''' <summary>
        ''' Encrypts the specified stream to memory using preset key and preset initialization vector
        ''' </summary>
        Public Function Encrypt(ByVal s As Stream) As CryptoData
            Dim ms As New IO.MemoryStream
            Dim b(_BufferSize) As Byte
            Dim i As Integer

            ValidateKeyAndIv(True)

            Dim cs As New CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write)
            i = s.Read(b, 0, _BufferSize)
            Do While i > 0
                cs.Write(b, 0, i)
                i = s.Read(b, 0, _BufferSize)
            Loop

            cs.Close()
            ms.Close()

            Return New CryptoData(ms.ToArray)
        End Function

        ''' <summary>
        ''' Decrypts the specified data using provided key and preset initialization vector
        ''' </summary>
        Public Function Decrypt(ByVal encryptedData As CryptoData, ByVal key As CryptoData) As CryptoData
            Me.Key = key
            Return Decrypt(encryptedData)
        End Function

        ''' <summary>
        ''' Decrypts the specified stream using provided key and preset initialization vector
        ''' </summary>
        Public Function Decrypt(ByVal encryptedStream As Stream, ByVal key As CryptoData) As CryptoData
            Me.Key = key
            Return Decrypt(encryptedStream)
        End Function

        ''' <summary>
        ''' Decrypts the specified stream using preset key and preset initialization vector
        ''' </summary>
        Public Function Decrypt(ByVal encryptedStream As Stream) As CryptoData
            Dim ms As New System.IO.MemoryStream
            Dim b(_BufferSize) As Byte

            ValidateKeyAndIv(False)
            Dim cs As New CryptoStream(encryptedStream, _
                _crypto.CreateDecryptor(), CryptoStreamMode.Read)

            Dim i As Integer
            i = cs.Read(b, 0, _BufferSize)

            Do While i > 0
                ms.Write(b, 0, i)
                i = cs.Read(b, 0, _BufferSize)
            Loop
            cs.Close()
            ms.Close()

            Return New CryptoData(ms.ToArray)
        End Function

        ''' <summary>
        ''' Decrypts the specified data using preset key and preset initialization vector
        ''' </summary>
        Public Function Decrypt(ByVal encryptedData As CryptoData) As CryptoData
            Dim ms As New System.IO.MemoryStream(encryptedData.Bytes, 0, encryptedData.Bytes.Length)
            Dim b() As Byte = New Byte(encryptedData.Bytes.Length - 1) {}

            ValidateKeyAndIv(False)
            Dim cs As New CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read)

            Try
                cs.Read(b, 0, encryptedData.Bytes.Length - 1)
            Catch ex As CryptographicException
                ' Throw New CryptographicException("Unable to decrypt data. The provided key may be invalid.", ex)
            Finally
                cs.Close()
            End Try
            Return New CryptoData(b)
        End Function

    End Class

    Public Class CryptoData
        Private _b As Byte()
        Private _MaxBytes As Integer = 0
        Private _MinBytes As Integer = 0
        Private _StepBytes As Integer = 0

        ''' <summary>
        ''' Determines the default text encoding across ALL Data instances
        ''' </summary>
        Public Shared DefaultEncoding As Text.Encoding = System.Text.Encoding.GetEncoding("Windows-1252")

        ''' <summary>
        ''' Determines the default text encoding for this Data instance
        ''' </summary>
        Public Encoding As Text.Encoding = DefaultEncoding

        ''' <summary>
        ''' Creates new, empty encryption data
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Creates new encryption data with the specified byte array
        ''' </summary>
        Public Sub New(ByVal b As Byte())
            _b = b
        End Sub

        ''' <summary>
        ''' Creates new encryption data with the specified string; 
        ''' will be converted to byte array using default encoding
        ''' </summary>
        Public Sub New(ByVal s As String)
            Me.Text = s
        End Sub

        ''' <summary>
        ''' Creates new encryption data using the specified string and the 
        ''' specified encoding to convert the string to a byte array.
        ''' </summary>
        Public Sub New(ByVal s As String, ByVal encoding As System.Text.Encoding)
            Me.Encoding = encoding
            Me.Text = s
        End Sub

        ''' <summary>
        ''' returns true if no data is present
        ''' </summary>
        Public ReadOnly Property IsEmpty() As Boolean
            Get
                If _b Is Nothing Then
                    Return True
                End If
                If _b.Length = 0 Then
                    Return True
                End If
                Return False
            End Get
        End Property

        ''' <summary>
        ''' allowed step interval, in bytes, for this data; if 0, no limit
        ''' </summary>
        Public Property StepBytes() As Integer
            Get
                Return _StepBytes
            End Get
            Set(ByVal Value As Integer)
                _StepBytes = Value
            End Set
        End Property

        ''' <summary>
        ''' allowed step interval, in bits, for this data; if 0, no limit
        ''' </summary>
        Public Property StepBits() As Integer
            Get
                Return _StepBytes * 8
            End Get
            Set(ByVal Value As Integer)
                _StepBytes = Value \ 8
            End Set
        End Property

        ''' <summary>
        ''' minimum number of bytes allowed for this data; if 0, no limit
        ''' </summary>
        Public Property MinBytes() As Integer
            Get
                Return _MinBytes
            End Get
            Set(ByVal Value As Integer)
                _MinBytes = Value
            End Set
        End Property

        ''' <summary>
        ''' minimum number of bits allowed for this data; if 0, no limit
        ''' </summary>
        Public Property MinBits() As Integer
            Get
                Return _MinBytes * 8
            End Get
            Set(ByVal Value As Integer)
                _MinBytes = Value \ 8
            End Set
        End Property

        ''' <summary>
        ''' maximum number of bytes allowed for this data; if 0, no limit
        ''' </summary>
        Public Property MaxBytes() As Integer
            Get
                Return _MaxBytes
            End Get
            Set(ByVal Value As Integer)
                _MaxBytes = Value
            End Set
        End Property

        ''' <summary>
        ''' maximum number of bits allowed for this data; if 0, no limit
        ''' </summary>
        Public Property MaxBits() As Integer
            Get
                Return _MaxBytes * 8
            End Get
            Set(ByVal Value As Integer)
                _MaxBytes = Value \ 8
            End Set
        End Property

        ''' <summary>
        ''' Returns the byte representation of the data; 
        ''' This will be padded to MinBytes and trimmed to MaxBytes as necessary!
        ''' </summary>
        Public Property Bytes() As Byte()
            Get
                If _MaxBytes > 0 Then
                    If _b.Length > _MaxBytes Then
                        Dim b(_MaxBytes - 1) As Byte
                        Array.Copy(_b, b, b.Length)
                        _b = b
                    End If
                End If
                If _MinBytes > 0 Then
                    If _b.Length < _MinBytes Then
                        Dim b(_MinBytes - 1) As Byte
                        Array.Copy(_b, b, _b.Length)
                        _b = b
                    End If
                End If
                Return _b
            End Get
            Set(ByVal Value As Byte())
                _b = Value
            End Set
        End Property

        ''' <summary>
        ''' Sets or returns text representation of bytes using the default text encoding
        ''' </summary>
        Public Property Text() As String
            Get
                If _b Is Nothing Then
                    Return ""
                Else
                    '-- need to handle nulls here; oddly, C# will happily convert
                    '-- nulls into the string whereas VB stops converting at the
                    '-- first null!
                    Dim i As Integer = Array.IndexOf(_b, CType(0, Byte))
                    If i >= 0 Then
                        Return Me.Encoding.GetString(_b, 0, i)
                    Else
                        Return Me.Encoding.GetString(_b)
                    End If
                End If
            End Get
            Set(ByVal Value As String)
                _b = Me.Encoding.GetBytes(Value)
            End Set
        End Property

        ''' <summary>
        ''' Sets or returns Hex string representation of this data
        ''' </summary>
        Public Property Hex() As String
            Get
                Return Utils.ToHex(_b)
            End Get
            Set(ByVal Value As String)
                _b = Utils.FromHex(Value)
            End Set
        End Property

        ''' <summary>
        ''' Sets or returns Base64 string representation of this data
        ''' </summary>
        Public Property Base64() As String
            Get
                Return Utils.ToBase64(_b)
            End Get
            Set(ByVal Value As String)
                _b = Utils.FromBase64(Value)
            End Set
        End Property

        ''' <summary>
        ''' Returns text representation of bytes using the default text encoding
        ''' </summary>
        Public Shadows Function ToString() As String
            Return Me.Text
        End Function

        ''' <summary>
        ''' returns Base64 string representation of this data
        ''' </summary>
        Public Function ToBase64() As String
            Return Me.Base64
        End Function

        ''' <summary>
        ''' returns Hex string representation of this data
        ''' </summary>
        Public Function ToHex() As String
            Return Me.Hex
        End Function

    End Class

    Friend Class Utils

        ''' <summary>
        ''' converts an array of bytes to a string Hex representation
        ''' </summary>
        Friend Shared Function ToHex(ByVal ba() As Byte) As String
            If ba Is Nothing OrElse ba.Length = 0 Then
                Return ""
            End If
            Const HexFormat As String = "{0:X2}"
            Dim sb As New StringBuilder
            For Each b As Byte In ba
                sb.Append(String.Format(HexFormat, b))
            Next
            Return sb.ToString
        End Function

        ''' <summary>
        ''' converts from a string Hex representation to an array of bytes
        ''' </summary>
        Friend Shared Function FromHex(ByVal hexEncoded As String) As Byte()
            If hexEncoded Is Nothing OrElse hexEncoded.Length = 0 Then
                Return Nothing
            End If
            Try
                Dim l As Integer = Convert.ToInt32(hexEncoded.Length / 2)
                Dim b(l - 1) As Byte
                For i As Integer = 0 To l - 1
                    b(i) = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16)
                Next
                Return b
            Catch ex As Exception
                Return Nothing
                'Throw New System.FormatException("The provided string does not appear to be Hex encoded:" & _
                '  Environment.NewLine & hexEncoded & Environment.NewLine, ex)
            End Try
        End Function

        ''' <summary>
        ''' converts from a string Base64 representation to an array of bytes
        ''' </summary>
        Friend Shared Function FromBase64(ByVal base64Encoded As String) As Byte()
            If base64Encoded Is Nothing OrElse base64Encoded.Length = 0 Then
                Return Nothing
            End If
            Try
                Return Convert.FromBase64String(base64Encoded)
            Catch ex As System.FormatException
                Return Nothing
                'Throw New System.FormatException("The provided string does not appear to be Base64 encoded:" & _
                '     Environment.NewLine & base64Encoded & Environment.NewLine, ex)
            End Try
        End Function

        ''' <summary>
        ''' converts from an array of bytes to a string Base64 representation
        ''' </summary>
        Friend Shared Function ToBase64(ByVal b() As Byte) As String
            If b Is Nothing OrElse b.Length = 0 Then
                Return ""
            End If
            Return Convert.ToBase64String(b)
        End Function




    End Class

End Namespace