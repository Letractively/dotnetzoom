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
Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public MustInherit Class WeatherNetwork
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lblScript As System.Web.UI.WebControls.Literal
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


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			


   			Title1.EditText = getlanguage("editer")

            Dim strCity As String = CType(Settings("city"), String)
            Dim strRegion As String = CType(Settings("region"), String)
            Dim strCountry As String = CType(Settings("country"), String)
			Dim strCode As String = ""
			if Not CType(settings("code"), String) Is Nothing then
			strCode =  CType(Settings("code"), String)
			end if


		' petit icone de météo média si jamais la ville et le pays sont pas conforme
		Select Case GetLanguage("N")
		Case "en"  
		lblScript.Text =  "<a href='http://www.theweathernetwork.com?ref=wxbtnsearch'><img src='http://www.theweathernetwork.com/weatherbuttons/images/150X90logo.gif' width='148' height='46' border='0' alt='theweathernetwork'></a>"
		case else
                    lblScript.Text = "<script language=javascript src='http://btn.meteomedia.ca/weatherbuttons/scripts/search120x60.js'></script>"
		end Select

            If CType(Settings("personalize"), Boolean) = True And Request.IsAuthenticated = True Then
                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))
                If dr.Read Then
                    strCity = dr("City").ToString
                    strRegion = dr("Region").ToString
                    strCountry = dr("Country").ToString
					'Get Code from DataBase
					
	            If strCity <> "" and strCountry <> "" Then
		            Dim DrW as SqlDataReader = GetMeteoCode(Stripstring(strCountry), Stripstring(strRegion), Stripstring(strCity))
        	        If drW.Read Then
						if drW("Code").ToString <> "" then
						strCode = drW("Code").ToString
						End If
					End if
                	drW.Close()
            	End If					
				
					
                End If
                dr.Close()
            End If

			If strCode <> "" then
				Select Case GetLanguage("N")
				Case "en"  	
                        lblScript.Text = "<iframe marginheight=""0"" marginwidth=""0"" name=""iframe" & ModuleId.ToString() & """ id=""iframe" & ModuleId.ToString() & """ width=""150"" height=""90"" src=""http://www.theweathernetwork.com/weatherbuttons/template5.php?placeCode=" & strCode & "&amp;multipleCity=0&amp;citySearch=0&amp;celsiusF=C&amp;cityNameNeeded=1&amp;category0=Cities&amp;celsiusF=C"" align=""top"" frameborder=""0"" scrolling=""no""></iframe>"
				case else
                        lblScript.Text = "<iframe marginheight=""0"" marginwidth=""0"" name=""iframe" & ModuleId.ToString() & """ id=""iframe" & ModuleId.ToString() & """ width=""150"" height=""90"" src=""http://www.meteomedia.com/weatherbuttons/template5.php?placeCode=" & strCode & "&amp;multipleCity=0&amp;citySearch=0&amp;celsiusF=C&amp;cityNameNeeded=1&amp;category0=Cities&amp;celsiusF=C"" align=""top"" frameborder=""0"" scrolling=""no""></iframe>"
				end select
			end if


 			lblScript.id = ""
        End Sub


	    End Class
End Namespace