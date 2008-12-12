Imports System.Globalization
Imports System.Web.Security
Imports System.Diagnostics
Imports System.Threading


Namespace DotNetZoom

    Public Class Enligne
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal

        Protected WithEvents Message As System.Web.UI.WebControls.Label
        Protected WithEvents IPMessage As System.Web.UI.WebControls.Label
        Protected WithEvents ServeurMessage As System.Web.UI.WebControls.Label
        Protected WithEvents lblAppRestarts As System.Web.UI.WebControls.Label
        Protected WithEvents lblFreeMem As System.Web.UI.WebControls.Label
		
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

			


            IPMessage.Text =  Request.UserHostAddress
            ServeurMessage.Text = RecupUtime
			Dim Int32 as integer

			Dim myConnection As New SqlConnection(GetDBConnectionString)
         
            myConnection.Open()
			
    Dim myCommand As New SqlCommand("select count(*) from SessionTracker", myConnection)
    Int32 = myCommand.ExecuteScalar()
    myConnection.Close()

			
	Message.Text = Int32.ToString()

            Try
                Dim perfAppRestarts As New PerformanceCounter("ASP.NET", "Application Restarts")
                Dim perfFreeMem As New PerformanceCounter("Memory", "Available MBytes")

                lblAppRestarts.Text = perfAppRestarts.NextValue()
                lblFreeMem.Text = perfFreeMem.NextValue() & " Mo"
                perfAppRestarts.Dispose()

                perfFreeMem.Dispose()
            Catch
                lblAppRestarts.Text = "0"
                lblFreeMem.Text = "0"
            End Try

        End Sub


public Function GetBrowserType() As String

Dim agt As String 
If Request.UserAgent <> "" then
agt = LCase(Request.UserAgent)
else
Return "Aucun"
end if



if (agt.indexOf("yahoo! slurp") <> -1) then 
return "Yahoo! Slurp"
end if

if (agt.indexOf("opera") <> -1) then 
return agt.substring(agt.indexOf("opera"), agt.Length() - agt.indexOf("opera")) 
end if
if (agt.indexOf("staroffice") <> -1) then
return "Star Office"
end if
if (agt.indexOf("beonex") <> -1) then 
return "Beonex" 
end if
if (agt.indexOf("chimera") <> -1) then
return "Chimera" 
end if
if (agt.indexOf("netpositive") <> -1) then 
return "NetPositive" 
end if
if (agt.indexOf("phoenix") <> -1) then 
return "Phoenix" 
end if
if (agt.indexOf("firefox") <> -1) then 
return agt.substring(agt.indexOf("firefox"), agt.Length() - agt.indexOf("firefox")) 
end if
if (agt.indexOf("safari") <> -1) then 
return "Safari" 
end if
if (agt.indexOf("skipstone") <> -1) then 
return "SkipStone" 
end if
if (agt.indexOf("msie") <> -1) then 
return "Internet Explorer " + Request.Browser.MajorVersion.ToString() + "." + Request.Browser.MinorVersion.ToString()
end if
if (agt.indexOf("netscape") <> -1) then 
return agt.substring(agt.indexOf("netscape"), agt.Length() - agt.indexOf("netscape"))  
end if
if (agt.indexOf("mozilla/5.0") <> -1) then 
return "Mozilla " 
end if
if (agt.indexOf("/") <> -1) then
	if (agt.substring(0,agt.indexOf("/")) <> "mozilla") then 
	return agt.substring(0,agt.indexOf("/"))
	 elseif (agt.indexOf(" ") <> -1) then
		return agt.substring(0,agt.indexOf(" "))
 			else return "Netscape" 
	end if
		else return agt
end if

End Function		
		
		

Private Function RecupUtime() As String
' Recupère l'Uptime de la machine
            Dim retour As String = ""
            Try
                Dim pc As New PerformanceCounter("System", "System Up Time")
                pc.NextValue()
                Dim ts As TimeSpan = TimeSpan.FromSeconds(pc.NextValue())
                retour = " " & TestZero(ts.Days, " " + GetLanguage("day"), True) & "<br>"
                retour &= " " & TestZero(ts.Hours, " " + GetLanguage("hour"), True) & "<br>"
                retour &= " " & TestZero(ts.Minutes, " " + GetLanguage("minute"), True) & "<br>"
                retour &= " " & TestZero(ts.Seconds, " " + GetLanguage("seconde"), True) & "<br>"
                retour &= " " & TestZero(ts.Milliseconds, " " + GetLanguage("millisecond"), True)
                pc.Dispose()
            Catch
            End Try
            Return retour
        End Function


Private Function TestZero(ByVal Lavaleur As Integer, _
ByVal LUnite As String, ByVal TestPlur As Boolean) As String
' Vérifie la valeur est supérieure à 1 et affiche le pluriel
If Lavaleur > 1 And TestPlur = True Then
Return Lavaleur & " " & LUnite & "s"
ElseIf Lavaleur > 0 Then
Return Lavaleur & " " & LUnite
End If
End Function

End Class

End Namespace