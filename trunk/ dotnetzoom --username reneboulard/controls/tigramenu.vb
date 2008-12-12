Imports System.Web.UI
imports System.Web
Imports System.IO
Imports System.Text

Namespace DotNetZoom


Public Class tigramenu
Inherits DotNetZoom.PortalModuleControl

Public tigraMenuString As String = ""

' Protected WithEvents lbltigraMenu As System.Web.UI.WebControls.Label

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


Private Function GetTPL() As String

       Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
       Dim strPageHTML As String = ""
	   Dim SkinFileName As String = ""

	   If IsAdminTab() or Request.Params("edit") <> "" then
						SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/tigraedit.js")
						strPageHTML = Context.Cache(SkinFileName)
						If strPageHTML = nothing then
							If Not File.Exists(SkinFileName) then
							SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
 								If Not File.Exists(SkinFileName) then
                            SkinFileName = Request.MapPath("/javascript/menu_tpl1.js")
								end if
							end if
						end if
						strPageHTML = Context.Cache(SkinFileName)
							' if page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As StreamReader
            				objStreamReader = File.OpenText(SkinFileName)
            				strPageHTML = objStreamReader.ReadToEnd
            				objStreamReader.Close()
							Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
	    					Cache.Insert(SkinFileName, strPageHTML, dep)
							' end  edit page
							end if
				else
					' get standard page
					
					If _portalSettings.ActiveTab.skin <> "" then
					Dim TempMenuFile As String 
					TempMenuFile = replace( _portalSettings.ActiveTab.skin, ".skin", "_menu.js")   
					SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/" & TempMenuFile)
					else
					SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
					end if
					strPageHTML = Context.Cache(SkinFileName)
        			    If strPageHTML Is Nothing Then
						'	Item not in cache, get it manually
							If Not File.Exists(SkinFileName) then
							SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
								If Not File.Exists(SkinFileName) then
                            SkinFileName = Request.MapPath("/javascript/menu_tpl1.js")
								end if
							end if
						strPageHTML = Context.Cache(SkinFileName)
							'  page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As StreamReader
            				objStreamReader = File.OpenText(SkinFileName)
            				strPageHTML = objStreamReader.ReadToEnd
            				objStreamReader.Close()
							Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
	      					Cache.Insert(SkinFileName, strPageHTML, dep)
							end if
					end if
			end if
Return strPageHTML
End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        ' Build list of tabs to be shown to user
        Dim authorizedTabs As New ArrayList()
        Dim addedTabs As Integer = 0
		Dim LastLevel As integer
		Dim DifLevel As integer
		DifLevel = 0
		LastLevel = 0

			
            Dim i As Integer
			Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N"))
            For i = 0 To DesktopTabs.Count - 1

                Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)

                Dim strTest As String = tab.TabName
                If tab.IsVisible Then
                    If PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True Then
						If DifLevel = Tab.Level then
						tab.Level = LastLevel
						end if
						DifLevel = Tab.Level
						If tab.level > (LastLevel) then
						tab.Level = (LastLevel +1)
						end If

						LastLevel = tab.Level
						authorizedTabs.Add(tab)
                        addedTabs += 1
                    End If
                End If

            Next i

            Dim objTab As TabStripDetails
 			' Ajout par René Boulard pour créer un fichier menu
			Dim TempString as StringBuilder = New System.Text.StringBuilder()
			Dim TempTabName as String
			 
			Dim NotFirstTimeAround As boolean
			TempString.append("<script language=""JavaScript"" type=""text/javascript""><!--" & ControlChars.Cr)

            ' read menu file
			
			TempString.append(GetTPL())
			TempString.append(";" & ControlChars.Cr)
			TempString.append("var MENU_ITEMS = [ " & ControlChars.Cr)
			LastLevel = 0
		
			
			TabLevel = 0
			NotFirstTimeAround = False
 			LastStop = False
			
			For Each objTab In authorizedTabs

			try
			
			If NotFirstTimeAround then
				If objTab.level <= LastLevel then
				For i = objTab.level to LastLevel
				if i <> objtab.level then TabLevel = TabLevel -1
				TempString.append(" ], " & ControlChars.Cr)
				next i			
				else
				TempString.append(" , ")
				TabLevel = TabLevel + 1
				end if
			end if
			
			
				TempString.append("[")


				
                If objTab.IconFile <> "" Then
                   TempString.append("Ti('" & objTab.IconFile &"') + ")
                End If

				
				TempTabName = objtab.TabName
				If _portalSettings.ActiveTab.TabId = objTab.TabId then
				' HighLight menu if active tab
				TempTabName = "<span class=""activetab"">" & TempTabName & "<\/span>"
				end If
				TempTabName = tempTabName.replace("'", "\'")			
				TempString.append("'" & TempTabName)
				If objTab.DisableLink Then
				TempString.append("' , '' , {'sb' : '" & TempTabName &"'} ")
				else
				TempString.append("' , '" & FormatFriendlyURL(objtab.FriendlyTabName, objTab.ShowFriendly, objtab.Tabid.ToString) & "' , {'sb' : '" & TempTabName &"'} ")
				end if
				NotFirstTimeAround = True
				LastLevel = objTab.Level
             Catch
             ' throws exception if the parent tab has not been loaded ( may be related to user role security not allowing access to a parent tab )
             End Try
           Next
		   
			For i = 0 to TabLevel
			TempString.append(" ]," & ControlChars.Cr)
			Next i
			   
			TempString.append(" ];" & ControlChars.Cr)
            TempString.append("function Ti (text) { return '<img src=""" & _portalSettings.UploadDirectory & "' + text + '"" border=""0"" alt=""""> ';}" & ControlChars.Cr)
            TempString.Append("function Ta (text) { return '<img src=""" & "images/" & "' + text + '"" border=""0"" alt=""""> ';}" & ControlChars.Cr)
			TempString.append("//--></SCRIPT>" & ControlChars.Cr)
            TempString.Append("<script language=""JavaScript"" type=""text/javascript"" src=""/javascript/menu.js""></script>" & ControlChars.Cr)
			TempString.append("<script language=""JavaScript"" type=""text/javascript""> new menu(MENU_ITEMS, MENU_POS1); </script>" & ControlChars.Cr)
			TempString.Append("<!-- tigra menu you can find a copy at http://www.softcomplex.com/products/tigra_menu/ = " & " -->")
			tigraMenuString = TempString.ToString()
	end sub	
	

End Class  
            
End Namespace