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

End Namespace