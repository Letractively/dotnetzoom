Imports System.IO
Namespace DotNetZoom
	Public Class TAGFileDownload
		Inherits System.Web.UI.Page

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
			'Put user code to initialize the page here
			Dim dnPath As String = Request.Params("File")
            Dim rootPath As String = GetAbsoluteServerPath(Request)
		 	dnPath = replace(dnPath, "\", "/")
		 	rootPath = replace(rootPath, "\", "/")
		 	rootPath = rootPath.Trim("/")
		 	dnPath = dnPath.replace(rootPath, "")
		 	' Response.redirect(dnpath, true)
		    Dim dnFile As FileInfo = New FileInfo(rootPath  + dnPath)

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(dnFile.Name))
		    Response.AddHeader("Content-Description", dnFile.Name)
		    Response.AddHeader("Content-Length", dnFile.Length.ToString)
		    Response.ContentType = "application/octet-stream"
		    Response.WriteFile(dnFile.FullName)
		    ' Response.Flush()
		   Response.End()

		End Sub

	End Class
End Namespace