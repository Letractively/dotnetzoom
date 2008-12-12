'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
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
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public Class EditWeatherNetwork
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents Address1 As Address
        Protected WithEvents chkPersonalize As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
		

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
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			Title1.DisplayHelp = "DisplayHelp_WeatherEdit"

            If Page.IsPostBack = False Then

                If ModuleId > 0 Then

                    Address1.ModuleId = ModuleId

                    ' Get settings from the database
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)

                    Address1.ShowStreet = False
                    Address1.ShowUnit = False
                    Address1.ShowPostal = False
                    Address1.ShowTelephone = False
                    Select Case LCase(CType(settings("country"), String))
                        Case "ca", "us"
                            Address1.CountryData = "Value"
                            Address1.RegionData = "Value"
                    End Select
                    Address1.Country = CType(settings("country"), String)
                    Address1.Region = CType(settings("region"), String)
                    Address1.City = CType(settings("city"), String)

                    chkPersonalize.Checked = CType(settings("personalize"), Boolean)

                End If

                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = ""
                End If

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim admin As New AdminDB()

            Select Case LCase(Address1.Country)
                Case "canada", "united states"
                    Address1.CountryData = "Value"
                    Address1.RegionData = "Value"
            End Select
            admin.UpdateModuleSetting(ModuleId, "country", Address1.Country)
            admin.UpdateModuleSetting(ModuleId, "region", Address1.Region)
            admin.UpdateModuleSetting(ModuleId, "city", Address1.City)
			
	            Dim DrW as SqlDataReader = GetMeteoCode(Address1.Country, Stripstring(Address1.Region), Stripstring(Address1.City))
                If drW.Read Then
				admin.UpdateModuleSetting(ModuleId, "code", drW("Code").ToString)
				End if
                drW.Close()
			
			
            admin.UpdateModuleSetting(ModuleId, "personalize", chkPersonalize.Checked.ToString)
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

	Private function Stripstring(ByVal StringToConvert As String) As String

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
		end if
		Return StringToConvert
		
	end function


       Public Function GetMeteoCode(ByVal Country As String, ByVal Region As String, ByVal City As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Country, Region, City})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return result

        End Function

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

    End Class

End Namespace