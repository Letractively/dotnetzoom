' Copyright (c) 2004-2008
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

Imports System.Net
Imports System.IO

Namespace DotNetZoom

    Public Class EditMapGoogle
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
		Protected WithEvents map As System.Web.UI.WebControls.Literal
        Protected WithEvents txtLocation As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtScript As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtgoogleAPI As System.Web.UI.WebControls.TextBox
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink
    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image

		Protected WithEvents txtLATLONG As System.Web.UI.WebControls.TextBox

        Protected WithEvents Address1 As Address
        Protected WithEvents chkDisplayMap As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkDisplayPointer As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkDisplayResize As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkDisplayType As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdLatLong As System.Web.UI.WebControls.LinkButton
		Protected WithEvents cmdGenerateScript As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
		Protected WithEvents valLocation As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents valScript As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents valgoogleAPI As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents valLatLong As System.Web.UI.WebControls.RequiredFieldValidator
		Protected WithEvents hypgetAPI As System.Web.UI.WebControls.HyperLink
		
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		Title1.DisplayHelp = "DisplayHelp_EditMapGoogle"
		valLocation.ErrorMessage = GetLanguage("need_location_MapGoogle")
		valgoogleAPI.ErrorMessage = GetLanguage("need_API_MapGoogle")
		valLatLong.ErrorMessage = GetLanguage("need_LatLong_MapGoogle")
		valScript.ErrorMessage = GetLanguage("need_Script_MapGoogle")
		chkDisplayMap.Text = getlanguage("MapGoogle_show_map_edit")
		chkDisplayPointer.Text = getlanguage("GoogleDisplayPointer")
		chkDisplayType.Text = getlanguage("GoogleDisplayType")
		chkDisplayResize.Text = getlanguage("GoogleDisplayResize")
		lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
		lnkicone.Text = GetLanguage("select_icone")	

			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdLatLong.Text = GetLanguage("GoogleGenerateLatLong")
			cmdGenerateScript.Text = GetLanguage("GoogleGenerateScript")
			hypgetAPI.Text = GetLanguage("GetGoogleAPI")
			hypgetAPI.NavigateURL = "http://www.google.com/apis/maps/signup.html"
            If Page.IsPostBack = False Then

                If ModuleId > 0 Then

                    Address1.ModuleId = ModuleId
                    ' Get settings from the database
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

                    Address1.ShowTelephone = False
                    txtLocation.Text = CType(settings("location"), String)
					txtgoogleAPI.Text = CType(settings("googleAPI"), String)
					txtLATLONG.Text = CType(settings("latlong"), String)
                    Address1.Unit = CType(settings("unit"), String)
                    Address1.Street = CType(settings("street"), String)
                    Address1.City = CType(settings("city"), String)
                    Address1.Region = CType(settings("region"), String)
                    Address1.Country = CType(settings("country"), String)
                    Address1.Postal = CType(settings("postalcode"), String)
                    chkDisplayMap.Checked = CType(settings("displaymap"), Boolean)
					chkDisplayPointer.Checked = CType(settings("displaypointer"), Boolean)
					chkDisplayType.Checked = CType(settings("displaytype"), Boolean)
					chkDisplayResize.Checked = CType(settings("displayresize"), Boolean)
					txticone.Text	= CType(settings("pointer"), String)
					map.text = CType(settings("script"), String)
					txtScript.Text = CType(settings("script"), String)
                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & TabId
            End If

			If TxtIcone.Text <> ""
			Dim ImageURL As STring
    		ImageUrl =  "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & _portalSettings.UploadDirectory 
    		If Not ImageUrl.EndsWith("/") Then
          		ImageUrl += "/"
   			End If
		  	myHtmlImage.ImageUrl = ImageUrl & TxtIcone.Text
		   	myHtmlImage.AlternateText = TxtIcone.Text
	   		myHtmlImage.ToolTip = TxtIcone.Text
			MyHtmlImage.Visible = True
			else
			MyHtmlImage.Visible = False
			end if
            lnkicone.NavigateUrl = "javascript:OpenNewWindow('" + tabID.ToString + "')"


        End Sub

		
        Private Sub cmdLatLong_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdLatLong.Click
			' GetLatLong
			Dim TempDirectionURL As STring = BuildDirectionsURL()
			txtLATLONG.Text = GetLATLONG(TempDirectionURL)
			If TxtLatLong.Text <> "" and TxtLatLong.Text <> "0,0" then
		    valLatLong.ErrorMessage = ""
		    valScript.ErrorMessage = ""
			txtScript.Text = makescript()
			map.text = txtScript.Text
			end if
			
			
		End Sub		
		
        Private Sub cmdGenerateScript_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdGenerateScript.Click
			' MakeScript
			If TxtLatLong.Text <> "" or TxtLatLong.Text <> "0,0" then
			txtScript.Text = makescript()
			map.text = txtScript.Text
			end if
		End Sub		


		
        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click


			If page.IsValid then
            ' Update settings in the database
            Dim admin As New AdminDB()
			

            admin.UpdateModuleSetting(ModuleId, "location", txtLocation.Text)
			admin.UpdateModuleSetting(ModuleId, "googleAPI", txtgoogleAPI.Text)
            admin.UpdateModuleSetting(ModuleId, "unit", Address1.Unit)
            admin.UpdateModuleSetting(ModuleId, "street", Address1.Street)
            admin.UpdateModuleSetting(ModuleId, "city", Address1.City)
            admin.UpdateModuleSetting(ModuleId, "region", Address1.Region)
            admin.UpdateModuleSetting(ModuleId, "country", Address1.Country)
            admin.UpdateModuleSetting(ModuleId, "postalcode", Address1.Postal)
            admin.UpdateModuleSetting(ModuleId, "displaymap", chkDisplayMap.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "displayresize", chkDisplayResize.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "displaytype", chkDisplayType.Checked.ToString)
            admin.UpdateModuleSetting(ModuleId, "displaypointer", chkDisplayPointer.Checked.ToString)
			admin.UpdateModuleSetting(ModuleId, "pointer", txticone.Text)
			

			If txtLATLONG.Text <> "0,0" then
            admin.UpdateModuleSetting(ModuleId, "latlong", txtLATLONG.Text)
			map.text = txtScript.Text
			admin.UpdateModuleSetting(Moduleid, "script", map.text)
			admin.UpdateModuleSetting(ModuleId, "directionURL", BuildGoogleURL())
			end if
			
			
			
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
			end if
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

		Private Function BuildGoogleURL() As String
		' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1
		    
			Dim strURL As String
			Dim GotOne As Boolean = False
            strURL = "http://maps.google.com/maps?f=q&amp;hl=" + GetLanguage("N") + "&amp;q="

            If Address1.Unit <> "" then
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Unit)
			GotOne = True
			end if
            if Address1.Street <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Street)
			GotOne = True
			end if
            if Address1.City <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.City)
			GotOne = True
			end if
            if Address1.Region <> GetLanguage("list_none") and  Address1.Region <> "" then
			If GotOne then strURL += "+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Region) 
			GotOne = True
			end if
            if Address1.Country <> GetLanguage("list_none") then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Country)
			GotOne = True
			end if
            if Address1.Postal <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Postal)
			GotOne = True
			end if
            BuildGoogleURL = strURL & "&amp;ie=UTF8&amp;z=15&amp;ll=" & txtLATLONG.Text

		
		
		End Function
		
		private Function makescript() As String
		
		
		
		
		Dim TempString As String
		
            TempString = "<script src=""http://maps.google.com/maps?file=api&amp;v=2&amp;key=" & txtgoogleAPI.Text & """" & " type=""text/javascript""></script>" & vbCrLf
        tempString +=  "<script type=""text/javascript"">"  & vbCrLf
		tempString +=  "   <!-- "  & vbCrLf
    	tempString +=  "  function load() { "   & vbCrLf
    	tempString +=  "        if (GBrowserIsCompatible()) { "   & vbCrLf
    	tempString +=  "            var map = new GMap2(document.getElementById(""map"")); "   & vbCrLf
    	tempString +=  "            map.setCenter(new GLatLng(" &  txtLATLONG.Text  & "), 15);" & vbCrLf


    	If chkDisplayPointer.Checked and txticone.Text <> "" then
		' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		Dim Admin As New AdminDB
		Dim dr As SqlDataReader = admin.GetSingleFile(txticone.Text, _portalSettings.PortalId)
		If dr.Read Then
    	tempString +=  "            var icon = new GIcon();" & vbCrLf
    	tempString +=  "            icon.image = """ & myHtmlImage.ImageUrl & """;" & vbCrLf
    	tempString +=  "            icon.iconSize = new GSize(" & dr("Width") & "," & dr("Height") & ");" & vbCrLf
    	tempString +=  "            icon.iconAnchor = new GPoint(6, 20);" & vbCrLf
    	tempString +=  "            icon.infoWindowAnchor = new GPoint(5, 1);" & vbCrLf
    	tempString +=  "            var point = new GLatLng(" & txtLATLONG.Text & ");" & vbCrLf
    	tempString +=  "            map.addOverlay(new GMarker(point, icon));" & vbCrLf
		end if
		dr.Close()
		end if
		
		if chkDisplayResize.Checked then
    	tempString +=  "            map.addControl(new GSmallMapControl());" & vbCrLf
		end if
		
		if chkDisplayType.Checked then
    	tempString +=  "            map.addControl(new GMapTypeControl());" & vbCrLf
		end if
		
		
		
		tempString +=  "        }"   & vbCrLf
		tempString +=  "      }"   & vbCrLf  & vbCrLf
		tempString +=  "   -->"   & vbCrLf 
		tempString +=  " </script>"   & vbCrLf 
		tempString +=  "<div id=""map"" style=""width: 400px; height: 200px""></div>" & vbCrLf 
		Return TempString
		end Function
		
        Private Function GetLATLONG(ByVal strURL As String) As String

		' http://maps.google.com/maps/geo?q=1600+Amphitheatre+Parkway,+Mountain+View,+CA&output=csv&key=ABQIAAAA-6yUObZEVxmGINrCkEK2LxRbLzMihHO-8yMdD1jfr4jexAQ9mhS4CanMiXCP5HtWvs14CnKCL3qD0A

            Try
                Dim objRequest As HttpWebRequest = WebRequest.Create(strURL)

                Dim objResponse As HttpWebResponse = objRequest.GetResponse()
                Dim sr As StreamReader
                sr = New StreamReader(objResponse.GetResponseStream())
                Dim strResponse As String = sr.ReadToEnd()
                sr.Close()
				' 200,8,37.423021,-122.083739
            Dim LatLongCont As Array
            LatLongCont = Split(strResponse, ",")
            Return LatLongCont(2) & "," & LatLongCont(3)
            Catch
                ' error accessing MapGoogle website
            End Try

        End Function

        Private Function BuildDirectionsURL() As String

            Dim strURL As String
			Dim GotOne As Boolean = False
            strURL = "http://maps.google.com/maps/geo?q="

            If Address1.Unit <> "" then
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Unit)
			GotOne = True
			end if
            if Address1.Street <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Street)
			GotOne = True
			end if
            if Address1.City <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.City)
			GotOne = True
			end if
            if Address1.Region <> GetLanguage("list_none") and  Address1.Region <> "" then
			If GotOne then strURL += "+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Region) 
			GotOne = True
			end if
            if Address1.Country <> GetLanguage("list_none") then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Country)
			GotOne = True
			end if
            if Address1.Postal <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(Address1.Postal)
			GotOne = True
			end if
            BuildDirectionsURL = strURL & "&amp;output=csv&amp;key=" & txtgoogleAPI.Text

        End Function		
		
		
    End Class

End Namespace