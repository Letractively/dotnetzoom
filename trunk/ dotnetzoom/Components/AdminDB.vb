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
Imports System.object
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Text.RegularExpressions

Namespace DotNetZoom


    '*********************************************************************
    '
    ' ModuleItem Class
    '
    ' This class encapsulates the basic attributes of a Module, and is used
    ' by the administration pages when manipulating modules.  ModuleItem implements
    ' the IComparable interface so that an ArrayList of ModuleItems may be sorted
    ' by ModuleOrder, using the ArrayList's Sort() method.
    '
    '*********************************************************************

    Public Class ModuleItem
        Implements IComparable


        Private _moduleOrder As Integer
        Private _title As String
        Private _pane As String
        Private _id As Integer
        Private _defId As Integer


        Public Property ModuleOrder() As Integer

            Get
                Return _moduleOrder
            End Get
            Set(ByVal Value As Integer)
                _moduleOrder = Value
            End Set

        End Property


        Public Property ModuleTitle() As String

            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set

        End Property


        Public Property PaneName() As String

            Get
                Return _pane
            End Get
            Set(ByVal Value As String)
                _pane = Value
            End Set

        End Property


        Public Property ModuleId() As Integer

            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set

        End Property


        Public Property ModuleDefId() As Integer

            Get
                Return _defId
            End Get
            Set(ByVal Value As Integer)
                _defId = Value
            End Set

        End Property


        Protected Overridable Function CompareTo(ByVal value As Object) As Integer Implements IComparable.CompareTo

            If value Is Nothing Then
                Return 1
            End If

            Dim compareOrder As Integer = CType(value, ModuleItem).ModuleOrder

            If Me.ModuleOrder = compareOrder Then Return 0
            If Me.ModuleOrder < compareOrder Then Return -1
            If Me.ModuleOrder > compareOrder Then Return 1
            Return 0

        End Function

    End Class


    '*********************************************************************
    '
    ' TabItem Class
    '
    ' This class encapsulates the basic attributes of a Tab, and is used
    ' by the administration pages when manipulating tabs.  TabItem implements
    ' the IComparable interface so that an ArrayList of TabItems may be sorted
    ' by TabOrder, using the ArrayList's Sort() method.
    '
    '*********************************************************************

    Public Class TabItem
        Implements IComparable

        Private _order As Integer
        Private _name As String
        Private _id As Integer
        Private _parent As Integer


        Public Property TabOrder() As Integer

            Get
                Return _order
            End Get
            Set(ByVal Value As Integer)
                _order = Value
            End Set
        End Property


        Public Property TabName() As String

            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property


        Public Property TabId() As Integer

            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set
        End Property


        Public Property ParentId() As Integer

            Get
                Return _parent
            End Get
            Set(ByVal Value As Integer)
                _parent = Value
            End Set
        End Property


        Public Overridable Function CompareTo(ByVal value As Object) As Integer Implements IComparable.CompareTo

            If value Is Nothing Then
                Return 1
            End If

            Dim compareOrder As Integer = CType(value, TabItem).TabOrder

            If Me.TabOrder = compareOrder Then Return 0
            If Me.TabOrder < compareOrder Then Return -1
            If Me.TabOrder > compareOrder Then Return 1
            Return 0

        End Function

    End Class


    '*********************************************************************
    '
    ' AdminDB Class
    '
    ' Class that encapsulates all data logic necessary to add/query/delete
    ' configuration, layout and security settings values within the Portal database.
    '
    '*********************************************************************

    Public Class AdminDB

        Public Function GetHostSetting(ByVal SettingName As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SettingName})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result
        End Function


        Public Sub UpdateHostSetting(ByVal SettingName As String, ByVal SettingValue As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {SettingName, SettingValue})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			HttpContext.Current.Cache.Remove(GetDBname & "HostSetting")
        End Sub

        Public Sub UpdateTimeZoneCodes(ByVal Language As String, ByVal Zone as Integer, ByVal Description As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Zone, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub
		
        Public Sub UpdateSiteLogReports(ByVal Language As String, ByVal Code as Integer, ByVal Description As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Code, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub UpdateBillingFrequencyCodes(ByVal Language As String, ByVal Code as String, ByVal Description As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Code, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub UpdateCurrencies(ByVal Language As String, ByVal Code as String, ByVal Description As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Code, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub
		
        Public Sub UpdateCountryCodes(ByVal Language As String, ByVal Code as String, ByVal Description As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Code, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub
		

        Public Function AddPortalInfo(ByVal Language As String, ByVal HomePage As Boolean, ByVal PortalName As String, ByVal PortalAlias As String, Optional ByVal Currency As String = "", Optional ByVal FirstName As String = "", Optional ByVal LastName As String = "", Optional ByVal Username As String = "", Optional ByVal Password As String = "", Optional ByVal Email As String = "", Optional ByVal ExpiryDate As String = "", Optional ByVal HostFee As Double = 0, Optional ByVal HostSpace As Double = 0, Optional ByVal SiteLogHistory As Integer = -1, <SqlParameter(, , , , , ParameterDirection.Output)> Optional ByVal PortalId As Integer = -1) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, HomePage, PortalName, PortalAlias, IIf(Currency <> "", Currency, "USD"), FirstName, LastName, Username, Password, Email, IIf(ExpiryDate <> "", ExpiryDate, SqlInt16.Null), HostFee, HostSpace, IIf(SiteLogHistory <> -1, SiteLogHistory, SqlInt16.Null), PortalId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            Return CInt(myCommand.Parameters("@PortalID").Value)
        End Function

		
        Public Sub UpdatePortalInfo(ByVal PortalId As Integer, ByVal PortalName As String, ByVal PortalAlias As String, Optional ByVal LogoFile As String = "", Optional ByVal FooterText As String = "", Optional ByVal UserRegistration As Integer = 0, Optional ByVal BannerAdvertising As Integer = 0, Optional ByVal Currency As String = "", Optional ByVal AdministratorId As Integer = -1, Optional ByVal ExpiryDate As String = "", Optional ByVal HostFee As Double = 0, Optional ByVal HostSpace As Double = 0, Optional ByVal PaymentProcessor As String = "", Optional ByVal ProcessorUserId As String = "", Optional ByVal ProcessorPassword As String = "", Optional ByVal Description As String = "", Optional ByVal KeyWords As String = "", Optional ByVal BackgroundFile As String = "", Optional ByVal SiteLogHistory As Integer = -1, Optional ByVal TimeZone As Integer = 0, Optional ByVal SSL As Boolean = False)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, PortalName, PortalAlias, IIf(LogoFile <> "", LogoFile, SqlInt16.Null), IIf(FooterText <> "", FooterText, SqlInt16.Null), UserRegistration, BannerAdvertising, IIf(Currency <> "", Currency, SqlInt16.Null), IIf(AdministratorId <> -1, AdministratorId, SqlInt16.Null), IIf(ExpiryDate <> "", ExpiryDate, SqlInt16.Null), HostFee, HostSpace, PaymentProcessor, ProcessorUserId, ProcessorPassword, Description, KeyWords, BackgroundFile, IIf(SiteLogHistory <> -1, SiteLogHistory, SqlInt16.Null), TimeZone, SSL})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetPortalAlias(ByVal PortalID As Integer) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result
        End Function

        Public Sub DeletePortalAlias(ByVal PortalAlias As String)
            ClearHostCache()
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalAlias})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub



        Public Sub UpdatePortalAlias(ByVal PortalId As Integer, ByVal PortalAlias As String, Optional ByVal SubPortal As Boolean = False, Optional ByVal ssl As Boolean = False)
            ClearHostCache()
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, PortalAlias, SubPortal, ssl})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub



        Public Sub DeletePortalInfo(ByVal PortalId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetPortals() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetSinglePortal(ByVal PortalId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Sub UpdatePortalExpiry(ByVal PortalId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub UpdatePortalTabOrder(ByVal objDesktopTabs As ArrayList, ByVal TabId As Integer, ByVal NewParentId As Integer, Optional ByVal Level As Integer = 0, Optional ByVal Order As Integer = 0, Optional ByVal IsVisible As String = "")

            Dim objTabs As New ArrayList()
            Dim objTab As TabStripDetails

            Dim intCounter As Integer
            Dim intFromIndex As Integer = -1
            Dim intOldParentId As Integer = -2
            Dim intToIndex As Integer = -1
            Dim intNewParentIndex As Integer = 0
            Dim intLevel As Integer
            Dim intAddTabLevel As Integer
            ' populate temporary tab array
            intCounter = 0
			
            For Each objTab In objDesktopTabs
                Dim strTest As String = objTab.TabName
                objTabs.Add(objTab)
				
                If objTab.TabId = TabId Then
                    intOldParentId = objTab.ParentId
                    intFromIndex = intCounter
                End If
                If objTab.TabId = NewParentId Then
                    intNewParentIndex = intCounter
                    intAddTabLevel = Int32.Parse(objTab.Level.ToString) + 1
                End If
                intCounter += 1
            Next

            ' adding new tab
            If intFromIndex = -1 Then
                objTab = New TabStripDetails()
                objTab.TabId = TabId
                objTab.ParentId = NewParentId
                objTab.IsVisible = Boolean.Parse(IsVisible)
                objTab.Level = intAddTabLevel
                objTabs.Add(objTab)
                intFromIndex = objTabs.Count - 1
            End If

            If IsVisible <> "" Then
                CType(objTabs(intFromIndex), TabStripDetails).IsVisible = Boolean.Parse(IsVisible)
            End If

            ' admin and hidden tabs cannot be hierarchical or ordered
            If CType(objTabs(intFromIndex), TabStripDetails).IsVisible = False Then
                CType(objTabs(intFromIndex), TabStripDetails).TabOrder = 9999
                CType(objTabs(intFromIndex), TabStripDetails).Level = 0
                CType(objTabs(intFromIndex), TabStripDetails).ParentId = -1
            Else
                ' if tab was deleted
                If NewParentId = -2 Then
                    objTabs.RemoveAt(intFromIndex)
                Else
                    ' if the parent change or we added a new non root level tab
                    If intOldParentId <> NewParentId And Not (intOldParentId = -2 And NewParentId = -1) Then
                        ' locate position of last child for new parent
                        If NewParentId <> -1 Then
                            intLevel = CType(objTabs(intNewParentIndex), TabStripDetails).Level
                        Else
                            intLevel = -1
                        End If

                        intCounter = intNewParentIndex + 1
                        While intCounter <= objTabs.Count - 1
                            If CType(objTabs(intCounter), TabStripDetails).Level > intLevel Then
                                intToIndex = intCounter
                            Else
                                Exit While
                            End If
                            intCounter += 1
                        End While
                        ' adding to parent with no children
                        If intToIndex = -1 Then
                            intToIndex = intNewParentIndex
                        End If
                        ' move tab
						' Add New Tab To end to Check
                        CType(objTabs(intFromIndex), TabStripDetails).ParentId = NewParentId
                        MoveTab(objTabs, intFromIndex, intToIndex, intLevel + 1)
                    Else
                        ' if level has changed
                        If Level <> 0 Then
                            intLevel = CType(objTabs(intFromIndex), TabStripDetails).Level

                            Dim blnValid As Boolean = True
                            Select Case Level
                                Case -1
                                    If intLevel = 0 Then
                                        blnValid = False
                                    End If
                                Case 1
                                    If intFromIndex > 0 Then
                                        If intLevel > CType(objTabs(intFromIndex - 1), TabStripDetails).Level Then
                                            blnValid = False
                                        End If
                                    Else
                                        blnValid = False
                                    End If
                            End Select

                            If blnValid Then
                                Dim intNewLevel As Integer
                                If Level = -1 Then
                                    intNewLevel = intLevel + Level
                                Else
                                    intNewLevel = intLevel
                                End If

                                ' get new parent
                                NewParentId = -2
                                intCounter = intFromIndex - 1
                                While intCounter >= 0 And NewParentId = -2
                                    If CType(objTabs(intCounter), TabStripDetails).Level = intNewLevel Then
                                        If Level = -1 Then
                                            NewParentId = CType(objTabs(intCounter), TabStripDetails).ParentId
                                        Else
                                            NewParentId = CType(objTabs(intCounter), TabStripDetails).TabId
                                        End If
                                        intNewParentIndex = intCounter
                                    End If
                                    intCounter -= 1
                                End While
                                CType(objTabs(intFromIndex), TabStripDetails).ParentId = NewParentId

                                If Level = -1 Then
                                    ' locate position of next child for parent
                                    intToIndex = -1
                                    intCounter = intNewParentIndex + 1
                                    While intCounter <= objTabs.Count - 1
                                        If CType(objTabs(intCounter), TabStripDetails).Level > intNewLevel Then
                                            intToIndex = intCounter
                                        Else
                                            Exit While
                                        End If
                                        intCounter += 1
                                    End While
                                    ' adding to parent with no children
                                    If intToIndex = -1 Then
                                        intToIndex = intNewParentIndex
                                    End If
                                Else
                                    intToIndex = intFromIndex - 1
                                    intNewLevel = intLevel + Level
                                End If

                                ' move tab
                                If intFromIndex = intToIndex Then
                                    CType(objTabs(intFromIndex), TabStripDetails).Level = intNewLevel
                                Else
                                    MoveTab(objTabs, intFromIndex, intToIndex, intNewLevel)
                                End If
                            End If
                        Else
                            ' if order has changed
                            If Order <> 0 Then
                                intLevel = CType(objTabs(intFromIndex), TabStripDetails).Level

                                ' find previous/next item for parent
                                intToIndex = -1
                                intCounter = intFromIndex + Order
                                While intCounter >= 0 And intCounter <= objTabs.Count - 1 And intToIndex = -1
                                    If CType(objTabs(intCounter), TabStripDetails).ParentId = NewParentId Then
                                        intToIndex = intCounter
                                    End If
                                    intCounter += Order
                                End While
                                If intToIndex <> -1 Then
                                    If Order = 1 Then
                                        ' locate position of next child for parent
                                        intNewParentIndex = intToIndex
                                        intToIndex = -1
                                        intCounter = intNewParentIndex + 1
                                        While intCounter <= objTabs.Count - 1
                                            If CType(objTabs(intCounter), TabStripDetails).Level > intLevel Then
                                                intToIndex = intCounter
                                            Else
                                                Exit While
                                            End If
                                            intCounter += 1
                                        End While
                                        ' adding to parent with no children
                                        If intToIndex = -1 Then
                                            intToIndex = intNewParentIndex
                                        End If
                                        intToIndex += 1
                                    End If
                                    MoveTab(objTabs, intFromIndex, intToIndex, intLevel, False)
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            ' update the tabs
            Dim intTabOrder As Integer
            Dim intDesktopTabOrder As Integer = -1
            For Each objTab In objTabs
                If objTab.IsVisible = False Then
                    intTabOrder = 9999
                Else
                    intDesktopTabOrder += 2
                    intTabOrder = intDesktopTabOrder
                End If
                UpdateTabOrder(objTab.TabId, intTabOrder, objTab.Level, objTab.ParentId)
            Next

        End Sub


        Private Sub MoveTab(ByVal objTabs As ArrayList, ByVal intFromIndex As Integer, ByVal intToIndex As Integer, ByVal intNewLevel As Integer, Optional ByVal blnAddChild As Boolean = True)
            Dim intCounter As Integer
            Dim objTab As TabStripDetails
            Dim blnInsert As Boolean

            Dim intOldLevel As Integer = CType(objTabs(intFromIndex), TabStripDetails).Level
            If intToIndex <> objTabs.Count Then
                blnInsert = True
            End If

            ' clone tab and children from old parent
            Dim objClone As New ArrayList()
            intCounter = intFromIndex
            While intCounter <= objTabs.Count - 1
                If CType(objTabs(intCounter), TabStripDetails).TabId = CType(objTabs(intFromIndex), TabStripDetails).TabId Or CType(objTabs(intCounter), TabStripDetails).Level > intOldLevel Then
                    objClone.Add(objTabs(intCounter))
                    intCounter += 1
                Else
                    Exit While
                End If
            End While

            ' remove tab and children from old parent
            objTabs.RemoveRange(intFromIndex, objClone.Count)
            If intToIndex > intFromIndex Then
                intToIndex -= objClone.Count
            End If

            ' add clone to new parent
            If blnInsert Then
                objClone.Reverse()
            End If
            Dim intIncrement As Integer = IIf(blnAddChild, 1, 0)
            For Each objTab In objClone
                If blnInsert and (intToIndex + intIncrement <= objTabs.Count) Then
                    objTab.Level += (intNewLevel - intOldLevel)
                    objTabs.Insert(intToIndex + intIncrement, objTab)
                Else
                    objTab.Level += (intNewLevel - intOldLevel)
                    objTabs.Add(objTab)
                End If
            Next
        End Sub

        Public Function AddTab(ByVal PortalId As Integer, ByVal TabName As String, ByVal ShowFriendly As Boolean, ByVal FriendlyTabName As String, ByVal AuthorizedRoles As String, ByVal LeftPaneWidth As String, ByVal RightPaneWidth As String, ByVal IsVisible As Boolean, ByVal DisableLink As Boolean, ByVal ParentId As Integer, ByVal IconFile As String, ByVal AdministratorRoles As String, <SqlParameter(, , , , , ParameterDirection.Output)> ByVal TabId As Integer, Optional ByVal css As String = "", Optional ByVal skin As String = "", Optional ByVal ssl As Boolean = False, Optional ByVal AltUrl As String = "") As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, TabName, ShowFriendly, convertstringtounicode(TabName), AuthorizedRoles, LeftPaneWidth, RightPaneWidth, IsVisible, DisableLink, IIf(ParentId <> -1, ParentId, SqlInt16.Null), IconFile, AdministratorRoles, TabId, IIf(css <> "", css, SqlInt16.Null), IIf(skin <> "", skin, SqlInt16.Null), ssl, AltUrl})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            Return CInt(myCommand.Parameters("@TabID").Value)
        End Function

	Public function convertstringtounicode(ByVal StringToConvert As String) As String

		StringToConvert = StringToConvert.Tolower
		if (Regex.IsMatch(StringToConvert, "[^a-z0-9]") = true ) then
		StringToConvert = Regex.Replace(StringToConvert, "[àáâãäåæ]" , "a")
		StringToConvert = Regex.Replace(StringToConvert, "[ç]" , "c")
		StringToConvert = Regex.Replace(StringToConvert, "[èéêë]" , "e")
		StringToConvert = Regex.Replace(StringToConvert, "[ìíîï]" , "i")
		StringToConvert = Regex.Replace(StringToConvert, "[ðòóôõö]" , "o")
		StringToConvert = Regex.Replace(StringToConvert, "[ñ]" , "n")
		StringToConvert = Regex.Replace(StringToConvert, "[ùúûü]" , "u")
		StringToConvert = Regex.Replace(StringToConvert, "[ýÿ]" , "y")
		StringToConvert = Regex.Replace(StringToConvert, "[þ]" , "p")
		StringToConvert = Regex.Replace(StringToConvert, "[^a-zA-Z0-9]" , "_")
		end if
		Return StringToConvert
		
	end function

		
		
        Public Sub UpdateTab(ByVal TabId As Integer, ByVal TabName As String, ByVal ShowFriendly As Boolean, ByVal FriendlyTabName As String, ByVal AuthorizedRoles As String, ByVal LeftPaneWidth As String, ByVal RightPaneWidth As String, ByVal IsVisible As Boolean, ByVal DisableLink As Boolean, ByVal ParentId As Integer, ByVal IconFile As String, ByVal AdministratorRoles As String, Optional ByVal css As String = "", Optional ByVal skin As String = "", Optional ByVal ssl As Boolean = False, Optional ByVal AltUrl As String = "")
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId, TabName, ShowFriendly, convertstringtounicode(TabName), AuthorizedRoles, LeftPaneWidth, RightPaneWidth, IsVisible, DisableLink, IIf(ParentId <> -1, ParentId, SqlInt16.Null), IconFile, AdministratorRoles, IIf(css <> "", css, SqlInt16.Null), IIf(skin <> "", skin, SqlInt16.Null), ssl, AltUrl})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub CopyTab(ByVal FromTabId As Integer, ByVal ToTabId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {FromTabId, ToTabId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ResetPortalSetting(_portalSettings.PortalID, 0)
			ResetPortalSetting(_portalSettings.PortalID, FromTabId)
			ResetPortalSetting(_portalSettings.PortalID, ToTabId)
        End Sub


        Public Sub UpdateTabOrder(ByVal TabId As Integer, ByVal TabOrder As Integer, ByVal Level As Integer, ByVal ParentId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId, TabOrder, Level, IIf(ParentId = -1, SqlInt16.Null, ParentId)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteTab(ByVal TabId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ResetPortalSetting(_portalSettings.PortalID, TabId)
        End Sub


        Public Function GetTabs(ByVal PortalId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

		Public Sub UpdateTabName(ByVal PortalId As Integer, ByVal TabID As Integer, ByVal TabName As String, ByVal Language As String)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, TabId, TabName, Language})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim TempKey as String = GetDBname & "TabsName_" & Language & "_" & PortalId.ToString
			Dim context As HttpContext = HttpContext.Current
			Context.Cache.Remove(TempKey)
		end sub

		Public Sub DeleteTabName(ByVal TabID As Integer, ByVal Language As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId, Language})
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim TempKey as String = GetDBname & "TabsName_" & Language & "_" & _portalSettings.PortalId.ToString
			Dim context As HttpContext = HttpContext.Current
			Context.Cache.Remove(TempKey)
		end sub
		
        Public Function GetTabsName(ByVal PortalId As Integer, ByVal Language As String) As Hashtable

			Dim TempKey as String = GetDBname & "TabsName_" & Language & "_" & PortalId.ToString
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as Hashtable
            _settings = CType(Context.Cache(TempKey), Hashtable)

             If _settings Is Nothing Then
            ' If this object has not been instantiated yet, we need to grab it
            _settings = New Hashtable()

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, Language})
            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While result.Read()
                _settings(result("TabId")) = result("TabName")
            End While
            result.Close()
            Context.Cache.Insert(TempKey, _settings, CDp(PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			end if
            Return _settings

        End Function
		
		

        Public Function GetTabById(ByVal TabId As Integer, ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetTabByName(ByVal TabName As String, Optional ByVal PortalId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabName, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetTabByFriendLyName(ByVal FriendlyTabName As String, Optional ByVal PortalId As Integer = -1) As Integer

            Dim TempKey As String = GetDBname() & "T_" & FriendlyTabName & "_" & PortalId.ToString
            Dim context As HttpContext = HttpContext.Current


            If context.Cache(TempKey) Is Nothing Then
                ' If this object has not been instantiated yet, we need to grab it
                Dim _settings As Integer
                ' Create Instance of Connection and Command Object
                Dim myConnection As New SqlConnection(GetDBConnectionString)

                ' Generate Command Object based on Method
                Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {FriendlyTabName, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If result.Read() Then
                    _settings = result("TabId")
                    result.Close()
                Else
                    result.Close()
                    Return -1
                End If


                context.Cache.Insert(TempKey, _settings, CDp(PortalId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                Return _settings
            Else

                Return context.Cache(TempKey)

            End If

            ' Create Instance of Connection and Command Object
            'Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            'Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            '   CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            '   New Object() {FriendlyTabName, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            'myConnection.Open()
            'Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            'Return result

        End Function


        Public Function GetTabsByParentId(ByVal ParentId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ParentId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Sub UpdateTabModuleOrder(ByVal TabId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ResetPortalSetting(_portalSettings.PortalID, 0)
			ResetPortalSetting(_portalSettings.PortalID, TabId)
        End Sub


        Public Function GetModule(ByVal ModuleId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Sub UpdateModuleOrder(ByVal ModuleId As Integer, ByVal ModuleOrder As Integer, ByVal PaneName As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, ModuleOrder, PaneName})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function AddModule(ByVal TabId As Integer, ByVal ModuleOrder As Integer, ByVal PaneName As String, ByVal ModuleTitle As String, ByVal ModuleDefId As Integer, ByVal CacheTime As Integer, ByVal EditRoles As String, ByVal ShowFriendly As Boolean, ByVal strLanguage As String, <SqlParameter(, , , , , ParameterDirection.Output)> Optional ByVal ModuleId As Integer = -1) As integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabId, ModuleOrder, PaneName, ModuleTitle, ModuleDefId, CacheTime, EditRoles, ShowFriendly, strLanguage, ModuleID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ResetPortalSetting(_portalSettings.PortalID, 0)
			ResetPortalSetting(_portalSettings.PortalID, TabId)
			
			Return CInt(myCommand.Parameters("@ModuleID").Value)
        End Function


        Public Sub UpdateModule(ByVal ModuleId As Integer, ByVal ModuleTitle As String, ByVal Alignment As String, ByVal Color As String, ByVal Border As String, ByVal IconFile As String, ByVal CacheTime As Integer, ByVal ViewRoles As String, ByVal EditRoles As String, ByVal ShowFriendly As Boolean, ByVal TabId As Integer, ByVal AllTabs As Boolean, ByVal ShowTitle As Boolean, ByVal Personalize As Integer, ByVal Container As String, ByVal Language As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, ModuleTitle, Alignment, Color, Border, IIf(IconFile <> "", IconFile, SqlInt16.Null), CacheTime, ViewRoles, EditRoles, ShowFriendly, TabId, AllTabs, ShowTitle, Personalize, IIf(Container <> "", Container, SqlInt16.Null), Language})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ResetPortalSetting(_portalSettings.PortalID, 0)
			ResetPortalSetting(_portalSettings.PortalID, TabId)

        End Sub


        Public Sub DeleteModule(ByVal ModuleId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
			ClearModuleCache(ModuleId)
            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub UpdatePortalModules(ByVal PortalId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

		
		Public Sub ResetPortalSetting(ByVal PortalNumber As Integer, ByVal TabId As Integer)

	    ' Reset cashe
			Dim TempKey as String = GetDBname & "Portal" & PortalNumber & "_Tab" & TabId
			Dim context As HttpContext = HttpContext.Current
			If Not Context.Cache(TempKey) Is Nothing Then
				context.Cache.Remove(TempKey)
			End If
		end sub

		Public Shared Function ConvertDataReaderToDataTable(ByVal reader As system.data.sqlclient.SqlDataReader) As system.data.DataTable 
				Dim table As System.Data.DataTable = reader.GetSchemaTable() 
 				Dim dt As System.Data.DataTable = New System.Data.DataTable () 
 				Dim dc As System.Data.DataColumn 
 				Dim row As System.Data.DataRow 
 				Dim al As System.Collections.ArrayList = New System.Collections.ArrayList () 
 				Dim i As Integer = 0 
 					While i < table.Rows.Count 
   					dc = New System.Data.DataColumn () 
   						If Not dt.Columns.Contains(table.Rows(i)("ColumnName").ToString()) Then 
     						dc.ColumnName = table.Rows(i)("ColumnName").ToString() 
     						dc.Unique = Convert.ToBoolean(table.Rows(i)("IsUnique")) 
     						dc.AllowDBNull = Convert.ToBoolean(table.Rows(i)("AllowDBNull")) 
     						dc.ReadOnly = Convert.ToBoolean(table.Rows(i)("IsReadOnly")) 
     						al.Add(dc.ColumnName) 
     						dt.Columns.Add(dc) 
   						End If 
   					i += 1 
 					End While 
 					While reader.Read() 
   					row = dt.NewRow() 
   					i = 0 
   					While i < al.Count 
     				row(CType(al(i), System.String)) = reader(CType(al(i), System.String)) 
     				i += 1 
   					End While 
   					dt.Rows.Add(row) 
 					End While 
 				Return dt 
		End Function
		
		
        Public Sub UpdateModuleSetting(ByVal ModuleId As Integer, ByVal SettingName As String, ByVal SettingValue As String)


	    ' Reset cashe 
			ClearModuleCache(ModuleId)
			Dim TempKey as String = GetDBname & "ModuleSettings_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as Hashtable
            _settings = CType(Context.Cache(TempKey), Hashtable)
			If Not _settings Is Nothing Then
				context.Cache.Remove(TempKey)
			End If




            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId, SettingName, SettingValue})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub UpdatePortalSetting(ByVal PortalId As Integer, ByVal SettingName As String, ByVal SettingValue As String)

	    	' Reset cashe 
			ClearPortalCache(PortalId)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, SettingName, SettingValue})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

		
		
        Public Sub UpdatelonglanguageSetting(ByVal Language As String, ByVal SettingName As String, ByVal SettingValue As String, Optional ByVal PortalId As Integer = -1)


	    ' Reset cashe 
			Dim TempKey as String
			if PortalID <> -1 then
			TempKey = GetDBname & "Llanguage_" & Language & "_" & SettingName & PortalID.ToString
			else
			TempKey = GetDBname & "Llanguage_" & Language & "_" & SettingName
			end if
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as String
            _settings = Context.Cache(TempKey)
			If Not _settings Is Nothing Then
				context.Cache.Remove(TempKey)
			End If

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, SettingName, SettingValue, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
            'update scriptfile
            If PortalId = -1 Then
                Dim objStream As StreamWriter
                objStream = File.AppendText(HttpContext.Current.Request.MapPath(glbPath + "Database/Newlanguage_" & Language & ".sql"))
                objStream.WriteLine("UpdatelonglanguageSetting '" & Language & "','" & SettingName & "','" & MakeSQLFriendly(SettingValue) & "', null ")
                objStream.WriteLine("GO")
                objStream.Close()
            End If
        End Sub

        Private Function MakeSQLFriendly(ByVal Item As String) As String
            Return Item.Replace("'", "''")
        End Function



        Public Function GetAvailablelanguage() As Hashtable
            Dim TempKey As String = GetDBname() & "Availanguage_"
            Dim context As HttpContext = HttpContext.Current
            Dim _settings As Hashtable
            _settings = CType(context.Cache(TempKey), Hashtable)

            If _settings Is Nothing Then
                ' If this object has not been instantiated yet, we need to grab it
                _settings = New Hashtable()


                ' Create Instance of Connection and Command Object
                Dim myConnection As New SqlConnection(GetDBConnectionString)
                ' Generate Command Object based on Method
                Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {})
                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

                While result.Read()
                    _settings(result.GetString(0)) = result.GetString(1)
                End While
                result.Close()
                context.Cache.Insert(TempKey, _settings, CDp(-1), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)

            End If
            Return _settings
        End Function
			
        Public Function GetlanguageContext(ByVal Language As String) As SqlDataReader

			
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
			
            Return result

        End Function

		Public Function GetNewlanguage(ByVal Language As String, ByVal SettingName As String) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, SettingName})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
			
            Return result

        End Function
				
        Public Function GetlanguageSettings(ByVal Language As String) As Hashtable

			Dim TempKey as String = GetDBname & "Slanguage_" & Language
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as Hashtable
            _settings = CType(Context.Cache(TempKey), Hashtable)

             If _settings Is Nothing Then
            ' If this object has not been instantiated yet, we need to grab it
            _settings = New Hashtable()
		

			
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            While result.Read()
                _settings(result.GetString(0)) = result.GetString(1)
            End While
            result.Close()
            Context.Cache.Insert(TempKey, _settings, CDp(-1), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			end if
			
            Return _settings

        End Function


        Public Sub UpdatelanguageSetting(ByVal Language As String, ByVal SettingName As String, ByVal SettingValue As String)


	    ' Reset cashe 
			Dim TempKey as String = GetDBname & "Slanguage_" & Language
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as Hashtable
            _settings = CType(Context.Cache(TempKey), Hashtable)
			If Not _settings Is Nothing Then
				context.Cache.Remove(TempKey)
			End If




            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, SettingName, SettingValue})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            'update scriptfile
            Dim objStream As StreamWriter
            objStream = File.AppendText(HttpContext.Current.Request.MapPath(glbPath + "/Database/Newlanguage_" & Language & ".sql"))
            objStream.WriteLine("updatelanguageSetting '" & Language & "','" & MakeSQLFriendly(SettingName) & "','" & MakeSQLFriendly(SettingValue) & "'")
            objStream.WriteLine("GO")
            objStream.Close()


        End Sub

        Public Sub UpdatelanguageContext(ByVal Language As String, ByVal SettingName As String, ByVal SettingValue As String, ByVal Context As String)


	    ' Reset cashe 
			Dim TempKey as String = GetDBname & "Slanguage_" & Language
			Dim tcontext As HttpContext = HttpContext.Current
			Dim _settings as Hashtable
            _settings = CType(tContext.Cache(TempKey), Hashtable)
			If Not _settings Is Nothing Then
				tcontext.Cache.Remove(TempKey)
			End If




            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, SettingName, SettingValue, Context})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            'update scriptfile
            Dim objStream As StreamWriter
            objStream = File.AppendText(HttpContext.Current.Request.MapPath(glbPath + "Database/Newlanguage_" & Language & ".sql"))
            objStream.WriteLine("updatelanguagecontext '" & Language & "','" & MakeSQLFriendly(SettingName) & "','" & MakeSQLFriendly(SettingValue) & "', '" & Context & "'")
            objStream.WriteLine("GO")
            objStream.Close()

        End Sub

		
        Public  Function GetlonglanguageSettings(ByVal Language As String, Optional ByVal PortalId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
			
            Return result 

        End Function

        Public Function GetSinglelonglanguageSettings(ByVal Language As String, ByVal SettingName As String, Optional ByVal PortalId As Integer = -1) As String

			Dim TempKey as String
			if PortalID <> -1 then
			TempKey = GetDBname & "Llanguage_" & Language & "_" & SettingName & PortalID.ToString
			else
			TempKey = GetDBname & "Llanguage_" & Language & "_" & SettingName
			end if
			Dim context As HttpContext = HttpContext.Current
			Dim _settings as String
            _settings = Context.Cache(TempKey)

             If _settings Is Nothing Then
		
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, SettingName, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            If result.Read()
			_settings = CType(result("SettingValue"), String)
            Else
			If PortalID = -1 then
			_settings = SettingName
			UpdatelonglanguageSetting(Language, SettingName, SettingName, PortalId)
                        If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                            SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", SettingName, String.Format(GetLanguage("LanguageERROR"), SettingName, GetLanguage("N")), "")
                        End If
			End if
			
            End If
            result.Close()
			If _settings <> "" then
            Context.Cache.Insert(TempKey, _settings, CDp(PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
			end if
			end if
			
            Return _settings

        End Function
		

        Public Function GetModuleDefinitions(ByVal PortalID As Integer, ByVal Language As String, Optional ByVal Admin As Boolean = True) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, Language, Admin})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Sub AddModuleDefinition(ByVal FriendlyName As String, ByVal DesktopSrc As String, ByVal HelpSrc As String, ByVal AdminOrder As String, ByVal EditSrc As String, ByVal Secure As Boolean, ByVal Description As String, ByVal EditModuleIcon As String, ByVal IsPremium As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {FriendlyName, IIf(DesktopSrc <> "", DesktopSrc, SqlInt16.Null), IIf(HelpSrc <> "", HelpSrc, SqlInt16.Null), IIf(AdminOrder <> "", AdminOrder, SqlInt16.Null), IIf(EditSrc <> "", EditSrc, SqlInt16.Null), Secure, Description, EditModuleIcon, IsPremium})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteModuleDefinition(ByVal ModuleDefId As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleDefId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub UpdateModuleDefinition(ByVal ModuleDefId As Integer, ByVal Language As String, ByVal FriendlyName As String, ByVal DesktopSrc As String, ByVal HelpSrc As String, ByVal AdminOrder As String, ByVal EditSrc As String, ByVal Secure As Boolean, ByVal Description As String, ByVal EditModuleIcon As String, ByVal IsPremium As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleDefId, Language, FriendlyName, IIf(DesktopSrc <> "", DesktopSrc, SqlInt16.Null), IIf(HelpSrc <> "", HelpSrc, SqlInt16.Null), IIf(AdminOrder <> "", AdminOrder, SqlInt16.Null), IIf(EditSrc <> "", EditSrc, SqlInt16.Null), Secure, Description, EditModuleIcon, IsPremium})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetSingleModuleDefinition(ByVal Language As String, ByVal ModuleDefId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, ModuleDefId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

		
        Public Function GetAdminModuleDefinition(ByVal Language As String, ByVal ModuleDefId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, ModuleDefId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function		
		
       Public Function GetAdminModuleDefinitions(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function
		
       Public Sub UpdateAdminModuleDefinitions(ByVal Language As String, ByVal ModuleDefID As integer, ByVal FriendlyName As String, ByVal Description As String ) 

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, ModuleDefID, FriendlyName, Description})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
 

        End Sub	
		
        Public Function GetSingleModuleDefinitionByName(ByVal Language As String, ByVal FriendlyName As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, FriendlyName})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetPortalModuleDefinitions(ByVal PortalID As Integer, ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetPortalModuleDefinitionFee(ByVal PortalID As Integer) As Double

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Dim dblHostFee As Double = 0
            If result.Read Then
                If Not IsDBNull(result("HostFee")) Then
                    dblHostFee = result("HostFee")
                End If
            End If
            result.Close()

            ' Return the datareader
            Return dblHostFee

        End Function


        Public Sub UpdatePortalModuleDefinition(ByVal PortalID As Integer, ByVal ModuleDefID As Integer, ByVal Subscribed As Boolean, ByVal HostFee As Double)

			ClearPortalCache(PortalId)
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, ModuleDefID, Subscribed, HostFee})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Function GetFiles(Optional ByVal PortalId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetSingleFile(ByVal FileName As String, Optional ByVal PortalId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {FileName, IIf(PortalId = -1, SqlInt16.Null, PortalId)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Sub DeleteFile(ByVal FileName As String, Optional ByVal PortalId As Integer = -1)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {FileName, IIf(PortalId = -1, SqlInt16.Null, PortalId)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub SynchronizeFiles(ByVal PortalId As Integer, ByVal strFolder As String)


            Dim strFileName As String
            Dim strExtension As String = ""
            Dim strContentType As String
            Dim imgImage As System.Drawing.Image
            Dim strWidth As String
            Dim strHeight As String

            DeleteFiles(PortalId)

            If System.IO.Directory.Exists(strFolder) Then
                AddDirectory(strFolder, GetFolderSizeRecursive(strFolder))
                Dim fileEntries As String() = System.IO.Directory.GetFiles(strFolder)
                For Each strFileName In fileEntries
                    If InStr(1, strFileName, ".") Then
                        strExtension = Mid(strFileName, InStrRev(strFileName, ".") + 1)
                    End If

                    Select Case strExtension.ToLower()
                        Case "txt" : strContentType = "text/plain"
                        Case "htm", "html" : strContentType = "text/html"
                        Case "rtf" : strContentType = "text/richtext"
                        Case "jpg", "jpeg" : strContentType = "image/jpeg"
                        Case "gif" : strContentType = "image/gif"
                        Case "bmp" : strContentType = "image/bmp"
                        Case "mpg", "mpeg" : strContentType = "video/mpeg"
                        Case "avi" : strContentType = "video/avi"
                        Case "pdf" : strContentType = "application/pdf"
                        Case "doc", "dot" : strContentType = "application/msword"
                        Case "csv", "xls", "xlt" : strContentType = "application/x-msexcel"
                        Case Else : strContentType = "application/octet-stream"
                    End Select

                    strHeight = ""
                    strWidth = ""

                    If InStr(1, glbImageFileTypes, strExtension) Then
                        Try
                            imgImage = System.Drawing.Image.FromFile(strFileName)
                            strHeight = imgImage.Height
                            strWidth = imgImage.Width
                            imgImage.Dispose()
                        Catch
                            ' error loading image file
                            strContentType = "application/octet-stream"
                        End Try
                    End If
                    AddFile(PortalId, Mid(strFileName, InStrRev(strFileName, "\") + 1), strExtension, New FileInfo(strFileName).Length, strWidth, strHeight, strContentType)
               
		        Next strFileName
            End If

        End Sub



        Public Sub DeleteFiles(Optional ByVal PortalId As Integer = -1)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub AddFile(ByVal PortalId As Integer, ByVal FileName As String, ByVal Extension As String, ByVal Size As String, ByVal Width As String, ByVal Height As String, ByVal ContentType As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null), FileName, Extension, Size, IIf(Width <> "", Width, SqlInt16.Null), IIf(Height <> "", Height, SqlInt16.Null), ContentType})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Private Function GetSomeFileSize(ByVal strFolder As String) As Double
            Dim SpaceUsedFile As Double
            Dim strFileName As String
            SpaceUsedFile = 0
            If System.IO.Directory.Exists(strFolder) Then
                Dim fileEntries As String() = System.IO.Directory.GetFiles(strFolder)
                For Each strFileName In fileEntries
		             SpaceUsedFile += New FileInfo(strFileName).Length
                Next strFileName
            End If
            Return SpaceUsedFile
        End Function


        Public Sub AddDirectory(ByVal strRoot As String, ByVal SpaceUsedDir As String)
            Dim IntSpaceUsedDir As Integer = 0
            Try
                IntSpaceUsedDir = Convert.ToInt32(Convert.ToInt64(SpaceUsedDir) / 1024)
            Catch ex As Exception
                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                    ' Function SendNotification(ByVal strFrom As String, ByVal strTo As String, ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, Optional ByVal strAttachment As String = "", Optional ByVal strBodyType As String = "")
                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", "Convert.ToInt32(SpaceUsedDir)", strRoot + " " + SpaceUsedDir + ControlChars.CrLf + ControlChars.CrLf + ex.ToString)
                End If

                Return
            End Try


            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {strRoot, IntSpaceUsedDir})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

        Public Function GetdirectorySpaceUsed(ByVal strRoot As String) As Double

            Dim TempSpaceUsed As Double
            TempSpaceUsed = 0
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {strRoot})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            If result.Read Then
                If Not IsDBNull(result("Size")) Then
                    TempSpaceUsed = result("Size") * 1024
                End If
            End If
            result.Close()

            If TempSpaceUsed = 0 Then
                TempSpaceUsed = GetFolderSizeRecursive(strRoot)
                AddDirectory(strRoot, TempSpaceUsed)
            End If
            Return TempSpaceUsed

        End Function

        Public Function GetFolderSizeRecursive(ByVal strRoot As String) As Double
            Dim SpaceUsedDir As Double
            SpaceUsedDir = 0
            If strRoot <> "" Then
                Dim strFolder As String
                If System.IO.Directory.Exists(strRoot) Then
                    For Each strFolder In System.IO.Directory.GetDirectories(strRoot)
                        SpaceUsedDir += GetFolderSizeRecursive(strFolder)
                    Next
                    SpaceUsedDir += GetSomeFileSize(strRoot)
                End If
            End If
            Return SpaceUsedDir
        End Function


        Public Function GetPortalSpaceUsed(Optional ByVal PortalId As Integer = -1) As Integer

            GetPortalSpaceUsed = 0

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId = -1, SqlInt16.Null, PortalId)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            If result.Read Then
                If Not IsDBNull(result("SpaceUsed")) Then
                    GetPortalSpaceUsed = result("SpaceUsed")
                End If
            End If
            result.Close()

        End Function


        Public Function GetCountryCodes(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetSingleCountry(ByVal Language As String, Optional ByVal Code As String = "", Optional ByVal Description As String = "") As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, IIf(Code <> "", Code, SqlInt16.Null), IIf(Description <> "", Description, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Dim strCountry As String = ""

            If result.Read Then
                strCountry = result("Country").ToString
            End If
            result.Close()

            Return strCountry

        End Function


        Public Function GetRegionCodes(ByVal Country As String, ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Country, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Sub UpdateRegionCodes(ByVal Language As String, ByVal Country As String, ByVal Code As String, ByVal Description As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, Country, Code, Description})

            ' Execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Function GetSingleRegion(ByVal Language As String, Optional ByVal Code As String = "", Optional ByVal Description As String = "") As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language, IIf(Code <> "", Code, SqlInt16.Null), IIf(Description <> "", Description, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Dim strRegion As String = ""

            If result.Read Then
                strRegion = result("Region").ToString
            End If
            result.Close()

            Return strRegion

        End Function


        Public Sub UpdateReferrer(ByVal Referrer As String, ByVal Description As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Referrer, Description})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Function GetSQL(ByVal SQL As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, SQL)

            ' Execute the command
            Try
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                ' Return the datareader
                Return result
            Catch objSQLException As SqlException
                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                    ' Function SendNotification(ByVal strFrom As String, ByVal strTo As String, ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, Optional ByVal strAttachment As String = "", Optional ByVal strBodyType As String = "")
                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", SQL, objSQLException.ToString)
                End If
            End Try






        End Function

        Public Function GetCurrencies(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetBillingFrequencyCodes(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function



        Public Function GetProcessorCodes() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetTimeZoneCodes(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function




        Public Sub UpdateClicks(ByVal TableName As String, ByVal KeyField As String, ByVal ItemId As Integer, Optional ByVal UserId As Integer = -1)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TableName, KeyField, ItemId, IIf(UserId <> -1, UserId, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetClicks(ByVal TableName As String, ByVal ItemId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TableName, ItemId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Sub AddPayPalIPN(ByVal PayPalID As String, ByVal PortalId As Integer, ByVal UserId As Integer, ByVal RoleID As Integer, ByVal Response As Boolean, ByVal RawData As String, ByVal ItemName As String, ByVal PortalRenew As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PayPalID, PortalId, UserId, RoleID, Response, RawData, ItemName, PortalRenew})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetSiteLog(ByVal PortalId As Integer, ByVal PortalAlias As String, Optional ByVal ReportType As Integer = -1, Optional ByVal StartDate As String = "", Optional ByVal EndDate As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, PortalAlias, IIf(ReportType <> -1, ReportType, SqlInt16.Null), IIf(StartDate <> "", StartDate, SqlInt16.Null), IIf(EndDate <> "", EndDate, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Public Function GetSiteLogGraph(ByVal PortalId As Integer, ByVal PortalAlias As String, Optional ByVal ReportType As Integer = -1, Optional ByVal StartDate As String = "", Optional ByVal EndDate As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalId, PortalAlias, IIf(ReportType <> -1, ReportType, SqlInt16.Null), IIf(StartDate <> "", StartDate, SqlInt16.Null), IIf(EndDate <> "", EndDate, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader()

            ' Return the datareader
            Return result

        End Function


        Public Function GetSiteLogReports(ByVal Language As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function GetSiteLogGraphTypes() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function


        Public Function ExecuteSQLScriptInAnotherDB(ByVal myConnectionString As String, ByVal strScript As String) As String

            Dim strSQL As String
            Dim strSQLExceptions As String = ""

            Dim arrSQL As String() = Split(strScript, "GO" & ControlChars.CrLf, , CompareMethod.Text)

            For Each strSQL In arrSQL
                ' execute script

                Dim myConnection As New SqlConnection(myConnectionString)
                Dim myCommand As New SqlCommand()
                myCommand.Connection = myConnection
                myCommand.CommandType = CommandType.Text
                myCommand.CommandText = strSQL
                myConnection.Open()

                Try
                    myCommand.ExecuteNonQuery()
                Catch objSQLException As SqlException
                    strSQLExceptions += "-------------SCRIPT --------------" & vbCrLf & strSQL & vbCrLf & "--------" & GetLanguage("error") & "-------" & vbCrLf & objSQLException.ToString & vbCrLf & vbCrLf
                    Dim ctx As HttpContext = HttpContext.Current
                    ctx.Server.ClearError()
                End Try

                myConnection.Close()

            Next

            Return strSQLExceptions

        End Function




        Public Function ExecuteSQLScript(ByVal strScript As String) As String

            Dim strSQL As String
            Dim strSQLExceptions As String = ""

            Dim arrSQL As String() = Split(strScript, "GO" & ControlChars.CrLf, , CompareMethod.Text)

            For Each strSQL In arrSQL
                ' execute script
                Dim myConnection As New SqlConnection(GetDBConnectionString)
                Dim myCommand As New SqlCommand()
                myCommand.Connection = myConnection
                myCommand.CommandType = CommandType.Text
                myCommand.CommandText = strSQL
                myConnection.Open()

                Try
                    myCommand.ExecuteNonQuery()
                Catch objSQLException As SqlException
                    strSQLExceptions += "-------------SCRIPT --------------" & vbCrLf & strSQL & vbCrLf & "--------" & GetLanguage("error") & "-------" & vbCrLf & objSQLException.ToString & vbCrLf & vbCrLf
                    Dim ctx As HttpContext = HttpContext.Current
                    ctx.Server.ClearError()
                End Try

                myConnection.Close()
            Next

            Return strSQLExceptions

        End Function

        ' --------------------------------------------------------------------
        ' InvokePopupCal - Accepts a target field ID as a paramter and returns
        ' a string formatted with a javascript call to invoke the Popup calendar.
        ' --------------------------------------------------------------------
        Public Shared Function InvokePopupCal(ByVal Field As System.Web.UI.WebControls.TextBox) As String

            ' Define character array to trim from language strings
            Dim TrimChars As Char() = {","c, " "c}

            ' Get culture array of month names and convert to string for
            ' passing to the popup calendar
            Dim MonthNameString As String = ""
            Dim Month As String
            For Each Month In DateTimeFormatInfo.CurrentInfo.MonthNames
                MonthNameString += Month & ","
            Next
            MonthNameString = MonthNameString.TrimEnd(TrimChars)

            ' Get culture array of day names and convert to string for
            ' passing to the popup calendar
            Dim DayNameString As String = ""
            Dim Day As String
            For Each Day In DateTimeFormatInfo.CurrentInfo.DayNames
                DayNameString += Day.Substring(0, 3) & ","
            Next
            DayNameString = DayNameString.TrimEnd(TrimChars)

            Return "javascript:popupCal('Cal','" & Field.ClientID & "','yyyy-MM-dd','" & MonthNameString & "','" & DayNameString & "' , '" & RTESafe(GetLanguage("return")) & "','" & RTESafe(GetLanguage("today")) & "');"

        End Function

    End Class

End Namespace