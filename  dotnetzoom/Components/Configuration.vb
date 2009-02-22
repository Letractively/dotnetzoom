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
Imports System.Configuration
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    '*********************************************************************
    '
    ' TabStripDetails Class
    '
    ' Class that encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    ' -- for a specific Tab in the Portal
    '
    '*********************************************************************

    Public Class TabStripDetails

        Public TabId As Integer
        Public TabName As String
		Public FriendlyTabName As String
		Public ShowFriendly As Boolean
        Public TabOrder As Integer
        Public AuthorizedRoles As String
        Public AdministratorRoles As String
        Public IsVisible As Boolean
        Public DisableLink As Boolean
        Public ParentId As Integer
        Public Level As Integer
        Public IconFile As String
        Public ssl As Boolean
        Public HasChildren As Boolean

    End Class


    '*********************************************************************
    '
    ' TabSettings Class
    '
    ' Class that encapsulates the detailed settings for a specific Tab 
    ' in the Portal
    '
    '*********************************************************************

    Public Class TabSettings

        Public TabId As Integer
        Public TabName As String
        Public TabOrder As Integer
        Public FriendlyTabName As String
		Public Css As String
        Public Skin As String
        Public ssl As Boolean
        Public AuthorizedRoles As String
        Public AdministratorRoles As String
        Public ShowFriendly As Boolean
        Public LeftPaneWidth As String
        Public RightPaneWidth As String
        Public IsVisible As Boolean
        Public DisableLink As Boolean
        Public ParentId As Integer
        Public Level As Integer
        Public IconFile As String
        Public HasChildren As Boolean
        Public Modules As New ArrayList()

    End Class


    '*********************************************************************
    '
    ' ModuleSettings Class
    '
    ' Class that encapsulates the detailed settings for a specific Tab 
    ' in the Portal
    '
    '*********************************************************************

    Public Class ModuleSettings

        Public ModuleId As Integer
        Public ModuleDefId As Integer
        Public TabId As Integer
        Public CacheTime As Integer
        Public ModuleOrder As Integer
        Public PaneName As String
        Public ModuleTitle As String
		Public IsAdminModule As Boolean
        Public AuthorizedEditRoles As String
        Public ShowFriendly As Boolean
        Public DesktopSrc As String
        Public HelpSrc As String
        Public AuthorizedViewRoles As String
        Public Alignment As String
        Public Color As String
        Public Border As String
        Public EditSrc As String
        Public IconFile As String
        Public EditModuleIcon As String
        Public Secure As Boolean
        Public AllTabs As Boolean
        Public ShowTitle As Boolean
        Public Personalize As Integer
		Public Language As String
        Public FriendlyName As String

    End Class


    '*********************************************************************
    '
    ' PortalSettings Class
    '
    ' This class encapsulates all of the settings for the Portal, as well
    ' as the configuration settings required to execute the current tab
    ' view within the portal.
    '
    '*********************************************************************

    Public Class PortalSettings

        Public PortalId As Integer
        Public GUID As String
        Public PortalAlias As String
        Public HTTP As String
        Public HTTPS As String
		Public PortalChild As Boolean
        Public PortalName As String
        Public UploadDirectory As String
        Public LogoFile As String
        Public FooterText As String
        Public ExpiryDate As String
        Public UserRegistration As Integer
        Public BannerAdvertising As Integer
        Public Currency As String
        Public AdministratorId As Integer
        Public Email As String
        Public HostFee As String
        Public HostSpace As Integer
        Public AdministratorRoleId As Integer
        Public RegisteredRoleId As Integer
        Public Description As String
        Public KeyWords As String
        Public BackgroundFile As String
        Public SiteLogHistory As Integer
		Public TimeZone As Integer
        Public SuperUserId As Integer
        Public Version As String
        Public BreadCrumbs As New ArrayList()
        Public ActiveTab As New TabSettings()
        Public Language As String
        Public SSL As Boolean
		
        '*********************************************************************
        '
        ' PortalSettings Constructor
        '
        ' The PortalSettings Constructor encapsulates all of the logic
        ' necessary to obtain configuration settings necessary to render
        ' a Portal Tab view for a given request.
        '
        '*********************************************************************

        Public Sub New(ByVal tabId As Integer, ByVal PortalAlias As String, ByVal ApplicationPath As String, ByVal Language As String)
			Me.Language = Language
            GetPortalSettings(tabId, PortalAlias, Language)

            Me.UploadDirectory = IIf(ApplicationPath = "/", "", ApplicationPath) & "/Portals/" & Me.GUID.ToString & "/"
            GetBreadCrumbsRecursively(Me.ActiveTab.TabId)
            Me.Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).ProductVersion.ToString
            Me.Version = Left(Me.Version, InStrRev(Me.Version, ".") - 1)

        End Sub

        Public Sub GetPortalSettings(ByVal tabId As Integer, ByVal PortalAlias As String, ByVal Language As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {tabId, PortalAlias, Language})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
			Dim objAdmin As New AdminDB()
            If result.Read() Then
                ' portal settings

                If GetHostSettings("chkEnableSSL").ToString = "Y" Then
                    Me.SSL = Boolean.Parse(result("ssl").ToString)
                Else
                    Me.SSL = False
                End If

                If HttpContext.Current.Request.IsSecureConnection Then
                    If Not IsDBNull(result("NOSSLAlias")) Then
                        Me.HTTP = AddHTTP(result("NOSSLAlias"))
                    Else
                        Me.HTTP = AddHTTP(PortalAlias)
                    End If
                    Me.HTTPS = AddHTTPS(PortalAlias)
                Else
                    Me.HTTP = AddHTTP(PortalAlias)
                    If Not IsDBNull(result("SSLAlias")) Then
                        Me.HTTPS = AddHTTPS(result("SSLAlias"))
                    Else
                        Me.HTTPS = Me.HTTP
                    End If
                End If

                    Me.PortalId = Int32.Parse(result("PortalID").ToString)
                    Me.GUID = result("GUID").ToString
                    Me.PortalAlias = result("PortalAlias").ToString
                    If InStr(1, Me.PortalAlias, "/") <> 0 Then
                        Me.PortalChild = True
                    Else
                        Me.PortalChild = False
                    End If
                    Me.PortalName = result("PortalName").ToString
                    Me.LogoFile = result("LogoFile").ToString
                    Me.FooterText = result("FooterText").ToString
                    Me.ExpiryDate = result("ExpiryDate").ToString
                    Me.UserRegistration = result("UserRegistration").ToString
                    Me.BannerAdvertising = result("BannerAdvertising").ToString
                    Me.Currency = result("Currency").ToString
                    Me.AdministratorId = result("AdministratorId").ToString
                    Me.Email = result("Email").ToString
                    Me.HostFee = result("HostFee").ToString
                    Me.HostSpace = IIf(IsDBNull(result("HostSpace")), 0, result("HostSpace"))
                    Me.AdministratorRoleId = result("AdministratorRoleId").ToString
                    Me.RegisteredRoleId = result("RegisteredRoleId").ToString
                    Me.Description = result("Description").ToString
                    Me.KeyWords = result("KeyWords").ToString
                    Me.BackgroundFile = result("BackgroundFile").ToString
                    Me.SiteLogHistory = IIf(IsDBNull(result("SiteLogHistory")), -1, result("SiteLogHistory"))
                    Me.TimeZone = IIf(IsDBNull(result("TimeZone")), -99, result("TimeZone"))
                    Me.SuperUserId = result("SuperUserId").ToString


                    '  tab settings
                    Me.ActiveTab.TabId = Int32.Parse(result("TabId").ToString)
                    Me.ActiveTab.TabOrder = IIf(IsDBNull(result("TabOrder")), -1, result("TabOrder"))
                    Me.ActiveTab.FriendlyTabName = IIf(IsDBNull(result("FriendlyTabName")), "", result("FriendlyTabName"))
                    Me.ActiveTab.css = IIf(IsDBNull(result("css")), "", result("css"))
                    Me.ActiveTab.skin = IIf(IsDBNull(result("skin")), "", result("skin"))
                    If Me.SSL Then
                        Me.ActiveTab.ssl = Boolean.Parse(result("tabssl").ToString)
                    Else
                        Me.ActiveTab.ssl = False
                    End If
                    Me.ActiveTab.AuthorizedRoles = result("AuthorizedRoles").ToString
                    Me.ActiveTab.AdministratorRoles = result("AdministratorRoles").ToString
                    Me.ActiveTab.TabName = result("TabName").ToString
                    Me.ActiveTab.ShowFriendly = Boolean.Parse(result("ShowFriendly").ToString)
                    Me.ActiveTab.LeftPaneWidth = result("LeftPaneWidth").ToString
                    Me.ActiveTab.RightPaneWidth = result("RightPaneWidth").ToString
                    Me.ActiveTab.IsVisible = Boolean.Parse(result("IsVisible").ToString)
                    Me.ActiveTab.DisableLink = Boolean.Parse(result("DisableLink").ToString)
                    Me.ActiveTab.ParentId = result("ParentId")
                    Me.ActiveTab.Level = result("Level")
                    Me.ActiveTab.IconFile = result("IconFile").ToString
                    Me.ActiveTab.HasChildren = Boolean.Parse(result("HasChildren").ToString)
                End If



            Dim DesktopTabs As ArrayList = Getportaltabs(Me.PortalId, Me.Language)
            If Me.ActiveTab.TabId = 0 And DesktopTabs.Count > 0 Then
                Me.ActiveTab.TabId = CType(DesktopTabs(0), TabStripDetails).TabId
            End If


            ' Read the next result --  Module Tab Information
            result.NextResult()

            While result.Read()

                Dim m As New ModuleSettings()

                m.ModuleId = Int32.Parse(result("ModuleID").ToString)
                m.ModuleDefId = Int32.Parse(result("ModuleDefID").ToString)
                m.TabId = Int32.Parse(result("TabID").ToString)
                m.PaneName = result("PaneName").ToString
                m.ModuleTitle = result("ModuleTitle").ToString
                m.IsAdminModule = False
                m.AuthorizedEditRoles = result("AuthorizedEditRoles").ToString
                m.CacheTime = Int32.Parse(result("CacheTime").ToString)
                m.ModuleOrder = Int32.Parse(result("ModuleOrder").ToString)
                m.ShowFriendly = Boolean.Parse(result("ShowFriendly").ToString)
                m.DesktopSrc = result("DesktopSrc").ToString
                m.HelpSrc = result("HelpSrc").ToString
                m.AuthorizedViewRoles = result("AuthorizedViewRoles").ToString
                m.EditSrc = result("EditSrc").ToString
                m.IconFile = result("IconFile").ToString
                m.EditModuleIcon = result("EditModuleIcon").ToString
                m.Secure = result("Secure")
                m.AllTabs = result("AllTabs")
                m.ShowTitle = result("ShowTitle")
                m.Personalize = result("Personalize")
                m.Language = result("Language").ToString
                m.FriendlyName = result("FriendlyName")

                Me.ActiveTab.Modules.Add(m)

            End While
            result.Close()
        End Sub

	
		
        Public Shared Function GetEditModuleSettings(ByVal ModuleId As Integer) As ModuleSettings

            ' Get Settings for this module from the database
            Dim _moduleSettings As New ModuleSettings()

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleId}, , "GetModule")

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            If result.Read() Then
                _moduleSettings.ModuleId = Int32.Parse(result("ModuleID").ToString)
                _moduleSettings.ModuleDefId = Int32.Parse(result("ModuleDefID").ToString)
                _moduleSettings.TabId = Int32.Parse(result("TabID").ToString)
                _moduleSettings.PaneName = "Edit"
                _moduleSettings.ModuleTitle = result("ModuleTitle").ToString
				_moduleSettings.IsAdminModule = False
                _moduleSettings.AuthorizedEditRoles = result("AuthorizedEditRoles").ToString
                _moduleSettings.EditSrc = result("EditSrc").ToString
                _moduleSettings.Secure = result("Secure")
                _moduleSettings.EditModuleIcon = result("EditModuleIcon").ToString
                _moduleSettings.ShowTitle = True
                _moduleSettings.Personalize = 2
                _moduleSettings.FriendlyName = result("FriendlyName").ToString
            End If
            result.Close()

            Return _moduleSettings

        End Function

        '*********************************************************************
        '
        ' GetModuleSettings Static Method
        '
        ' The PortalSettings.GetModuleSettings Method returns a hashtable of
        ' custom module specific settings from the database.  This method is
        ' used by some user control modules (Xml, Image, etc) to access misc
        ' settings.
        '
        '*********************************************************************

        Public Shared Function GetModuleSettings(ByVal ModuleId As Integer) As Hashtable

            ' Get Settings for this module from the database
            			
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim TempKey as String = GetDBname & "ModuleSettings_" & CStr(ModuleId)
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
                New Object() {ModuleId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            While result.Read()
                _settings(result.GetString(0)) = result.GetString(1)
            End While
            result.Close()
            Context.Cache.Insert(TempKey, _settings, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.TabID, ModuleId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
            End If
			
            Return _settings

        End Function

        '*********************************************************************
        '
        ' GetPortalSettings Static Method
        '
        ' The portalSettings.GetSiteSettings(_portalSettings.PortalID) Method returns a hashtable of
        ' custom settings from the database. 
        '
        '*********************************************************************

		Public Shared Function Getportaltabs(ByVal PortalId As Integer, ByVal Language As String) as ArrayList
			Dim TempKey as String = GetDBname & "_" & GetLanguage("N") & "_portaltabs_" & CStr(PortalId) 
			Dim context As HttpContext = HttpContext.Current
			If Context.Cache(TempKey) Is Nothing Then
            ' If this object has not been instantiated yet, we need to grab it
           	Dim DesktopTabs as New ArrayList()

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
                    Dim tabDetails As New TabStripDetails()
                    tabDetails.TabId = Int32.Parse(result("TabId").ToString)
                    tabDetails.TabName = result("TabName").ToString
                    tabDetails.FriendlyTabName = IIf(IsDBNull(result("FriendlyTabName")), "", result("FriendlyTabName"))
                    tabDetails.ShowFriendly = Boolean.Parse(result("ShowFriendly").ToString)
                    tabDetails.TabOrder = IIf(IsDBNull(result("TabOrder")), -1, result("TabOrder"))
                    tabDetails.AuthorizedRoles = result("AuthorizedRoles").ToString
                    tabDetails.AdministratorRoles = result("AdministratorRoles").ToString
                    tabDetails.IsVisible = Boolean.Parse(result("IsVisible").ToString)
                    tabDetails.DisableLink = Boolean.Parse(result("DisableLink").ToString)
                    tabDetails.ParentId = result("ParentId")
                    tabDetails.Level = result("Level")
                    tabDetails.ssl = Boolean.Parse(result("ssl").ToString)
                    tabDetails.IconFile = result("IconFile").ToString
                    tabDetails.HasChildren = Boolean.Parse(result("HasChildren").ToString)
                    DesktopTabs.Add(tabDetails)
            End While
            result.Close()
            Context.Cache.Insert(TempKey, DesktopTabs, CDp(PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
            Return DesktopTabs
			else
			Return CType(Context.Cache(TempKey), ArrayList)
			End If
            
		end Function

        Public Shared Function GetSiteSettings(ByVal PortalId As Integer) As Hashtable

			Dim TempKey as String = GetDBname & "SiteSettings_" & CStr(PortalId)
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
                New Object() {PortalId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            While result.Read()
                _settings(result.GetString(0)) = result.GetString(1)
            End While
            result.Close()
            Context.Cache.Insert(TempKey, _settings, CDp(PortalID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
            End If
			
            Return _settings

        End Function

        Public Shared Function GetHostSettings() As Hashtable

			Dim TempKey as String = GetDBname & "HostSetting"
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
                New Object() {})

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

		
        Public Shared Function GetPortalByAlias(ByVal PortalAlias As String) As Integer

            GetPortalByAlias = -1

		If PortalAlias <> "" then
			Dim TKey as String = GetDBname & "Alias_" & PortalAlias
			Dim context As HttpContext = HttpContext.Current

	       If Context.Cache(TKey) Is Nothing Then
			' Item not in cache, get it manually
            ' Create Instance of Connection and Command Object
              Dim myConnection As New SqlConnection(GetDBConnectionString)
                ' Generate Command Object based on Method
                Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {PortalAlias})

                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

                If result.Read() Then
                    If Not IsDBNull(result("PortalId")) Then
                        GetPortalByAlias = result("PortalID")
			            Context.Cache.Insert(TKey, GetPortalByAlias, CDp(result("PortalID")), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
                    End If
                End If
                result.Close()
			Else
			GetPortalByAlias = Context.Cache(TKey)
  			End If
		end if
            Return GetPortalByAlias

        End Function

        Public Shared Function GetPortalByTab(ByVal TabID As Integer, ByVal PortalAlias As String) As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TabID, PortalAlias})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            GetPortalByTab = Nothing

            If result.Read() Then
                If Not IsDBNull(result("PortalAlias")) Then
                    GetPortalByTab = result("PortalAlias")
                End If
            End If
            result.Close()

            Return GetPortalByTab

        End Function


        Private Sub GetBreadCrumbsRecursively(ByVal intTabId As Integer)
            Dim objAdmin As New AdminDB()
						
            Dim dr As SqlDataReader = objAdmin.GetTabById(intTabId, Me.Language)
            While dr.Read
                If Not IsDBNull(dr("ParentId")) Then
                    Dim tabDetails As New TabStripDetails()

                    tabDetails.TabId = intTabId.ToString
					
					tabDetails.TabName = dr("TabName").ToString
                    Me.BreadCrumbs.Insert(0, tabDetails)
                    GetBreadCrumbsRecursively(dr("ParentId"))
                Else ' root tabid
                    Dim tabDetails As New TabStripDetails()

                    tabDetails.TabId = intTabId.ToString
						tabDetails.TabName = dr("TabName").ToString
                    Me.BreadCrumbs.Insert(0, tabDetails)
                End If
            End While
            dr.Close()

        End Sub

        Public Shared Function GetVersion() As Integer

			Dim TempKey as String = GetDBname

       		Dim context As HttpContext = HttpContext.Current
	
			if Context.Cache(TempKey) is nothing then
        		
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' check if database exists
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, "select * from dbo.sysobjects where id = object_id(N'[dbo].[version]')")

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch
                return -2
            End Try

                ' Generate Command Object based on Method
                myCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {}, CommandType.Text, "select 'Build' = max(Build) from Version")

                Try
                    ' Execute the command
                    myConnection.Open()
                    Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                    If result.Read Then
                        If Not IsDBNull(result("Build")) Then
                            GetVersion = result("Build")
                        End If
                    End If
                    result.Close()
                Catch
                ' table does not exist - GetVersion = -1
				Return -1
                End Try
			Context.Cache.insert(TempKey, GetVersion, Nothing)
			else
			GetVersion = Context.Cache(TempKey) 
		end if

        End Function


        Public Shared Sub UpdateVersion(ByVal Build As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Build})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch
                ' stored procedure does not exist
            End Try
			Dim TempKey as String = GetDBname
    		Dim context As HttpContext = HttpContext.Current
			Context.Cache.Remove(TempKey) 
        End Sub


        Public Shared Function FindVersion(ByVal intVersion As Integer) As Boolean

            FindVersion = False

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {}, CommandType.Text, "select 1 from Version where Build = " & intVersion.ToString)

            Try
                ' Execute the command
                myConnection.Open()
                Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If result.Read Then
                    FindVersion = True
                End If
                result.Close()
            Catch
                ' table does not exist
            End Try

        End Function

    End Class

End Namespace