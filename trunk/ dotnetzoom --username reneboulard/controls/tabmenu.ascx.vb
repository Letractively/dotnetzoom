Imports System.Text

Namespace DotNetZoom

    Public MustInherit Class tabmenu
	       Inherits DotNetZoom.PortalModuleControl

		   Protected WithEvents TabMenuString As System.Web.UI.WebControls.Literal

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

	
	
		Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.load
  
        	Dim TempString as StringBuilder = New System.Text.StringBuilder()
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            ' Build list of tabs to be shown to user
            Dim authorizedTabs As New ArrayList()
          	Dim DesktopTabs As ArrayList = portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N"))
            Dim i As Integer
            For i = 0 To DesktopTabs.Count - 1
                Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
                If tab.IsVisible Then
                    If PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True Then
					
                        authorizedTabs.Add(tab)
					
                    End If
                End If
            Next i


            Dim objTab As TabStripDetails
			Dim NextTab As TabStripDetails
			Dim LastLevel As integer = -1
			Dim NextLevel As integer = 0
			Dim TabLevel  As integer = 0
			Dim DifLevel As integer  = 0
			Dim Vbtabint As integer = 0
			Dim IntTab As integer = 0
			Dim TempClass As String = ""
			Dim IntMenu As integer = 0
 
            ' For Each objTab In authorizedTabs
			TempString.Append("<table cellpadding=""0"" cellspacing=""0"" border=""0"">" & vbCrLf & "<tr>" & vbCrLf)
			For intTab = 0 To authorizedTabs.Count - 1
			ObjTab = CType(authorizedTabs(intTab), TabStripDetails)

			
			If objtab.level > LastLevel or objtab.level = 0 then
			if objtab.level > LastLevel then
			TempString.append(vbCrLf )
			end if
  		  	for Vbtabint = 0 to objtab.Level
	  		TempString.append(vbTab)
	  		Next
			
			If Objtab.level = 0 then
			NextLevel = 0
			IntMenu = IntMenu + 1
			TempString.Append("<td class=""menu"">" & vbCrLf & vbtab)
			End if
			If Objtab.level = 0 or Objtab.level = 1 then
			TempString.append("<ul id=""menu-" & IntMenu & "-" & objtab.Level & """>" & vbCrLf )
			else
			TempString.append("<ul id=""menu-" & IntMenu & "-" & objtab.Level & "-" & NextLevel & """>" & vbCrLf )
			NextLevel = NextLevel + 1
			end if
			End if
			
		
	 		for Vbtabint = 0 to objtab.Level + 1
	  		TempString.append(vbTab)
	  		Next
			

			

			if IntTab + 1 <= authorizedTabs.Count - 1 then
			NextTab = CType(authorizedTabs(intTab + 1), TabStripDetails)
			else
			NextTab = CType(authorizedTabs(authorizedTabs.Count - 1), TabStripDetails)
			end if
			
			TempClass = ""
			If NextTab.Level > ObjTab.Level then
			If objTab.Level = 0 then
			TempClass = " class=""top_parent"" "
			else
			TempClass = " class=""parent"" "
			end if
			end if
			

			if objTab.DisableLink Then
			   TempString.append( "<li class=""menu-" & IntMenu & "-" & objtab.Level & """><a " & TempClass & "href="""">" ) 
            Else
               TempString.append( "<li class=""menu-" & IntMenu & "-" & objtab.Level & """><a " & TempClass & "href=""" & FormatFriendlyURL(objtab.FriendlyTabName, objTab.ShowFriendly, objtab.Tabid.ToString) & """>")
            End If
			
			
            If objTab.IconFile <> "" Then
                TempString.append("<img src=""" & _portalSettings.UploadDirectory & objTab.IconFile & """ border=""0"" alt=""""> ")
            End If


			TempString.append(objTab.TabName &  "</a>")
			
			LastLevel = objtab.level
			
		
			if LastLevel >= Nexttab.level then
			TempString.append( "</li>" & vbCrLf)
			end if


			if LastLevel > Nexttab.level then
			for DifLevel = 1 to LastLevel - Nexttab.level
		    		for Vbtabint = 0 to (LastLevel - DifLevel) + 1
		 	    	TempString.append(vbTab)
		  			Next
				TempString.append("</ul>" & vbCrLf)
					for Vbtabint = 0 to (LastLevel - DifLevel) + 1
		 			TempString.append(vbTab)
		  			Next
				TempString.append("</li>" & vbCrLf)
			Next
			end if
	
			if Nexttab.Level = 0 then
			TempString.append(vbTab & "</ul>" & vbCrLf)
			TempString.Append(vbTab & "</td>" & vbCrLf)
			end if
			
			
			Next
			
			If LastLevel > 0 then
			for DifLevel = 0 to LastLevel
		    		for Vbtabint = 0 to (LastLevel - DifLevel)
		 	    	TempString.append(vbTab)
		  			Next
				TempString.append("</ul>" & vbCrLf)
			Next
			TempString.Append("</td>" & vbCrLf )
			End If
			TempString.Append( "</tr>"  & vbCrLf & "</table>" & vbCrLf)
		TabMenuString.Text = TempString.ToString()
	end sub		

    End Class

End Namespace