Imports System.IO
Imports System.Web.UI.WebControls
Imports DotNetZoom


Namespace DotNetZoom
	Public MustInherit Class TAGFileExplorer
		Inherits System.Web.UI.UserControl

		Protected WithEvents tblHeader As System.Web.UI.WebControls.Table
		Protected WithEvents lblErr As System.Web.UI.WebControls.Label
		Protected WithEvents chkSelectAll As System.Web.UI.WebControls.CheckBox
		Protected WithEvents grdFileFolders As System.Web.UI.WebControls.DataGrid
		Protected WithEvents chkSortTypeup As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkSortTypedown As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkSortNameup As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkSortNamedown As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkSortSizeup As System.Web.UI.WebControls.LinkButton
		Protected WithEvents chkSortSizedown As System.Web.UI.WebControls.LinkButton

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

#Region " Constants "
		Private extensions() As String = { _
		 ".arj", ".asa", ".asax", ".ascx", ".asmx", ".asp", ".aspx", ".au", _
		 ".avi", ".bat", ".bmp", ".cab", ".chm", ".com", ".config", ".cs", _
		 ".css", ".disco", ".dll", ".doc", ".exe", ".gif", ".hlp", ".htm", _
		 ".html", ".jpg", ".inc", ".ini", ".log", ".mdb", ".mid", ".midi", _
		 ".mov", ".mp3", ".mpg", ".mpeg", ".pdf", ".ppt", ".sys", ".txt", _
		 ".tif", ".vb", ".vbs", ".vsdisco", ".wav", ".wri", ".xls", ".xml", _
		 ".zip", ".flv"}
		Private textExtensions() As String = { _
		 ".asa", ".asax", ".ascx", ".asmx", ".asp", ".aspx", ".bat", _
		 ".config", ".cs", ".css", ".disco", ".htm", ".html", ".inc", _
		 ".ini", ".log", ".sys", ".txt", ".vb", ".vbs", ".vsdisco", _
		 ".xls", ".xml"}


		Private Enum GridColumn
			ColumnCheck
			ColumnIcon
			ColumnName
			ColumnType
			ColumnSize
			ColumnCreate
			ColumnModify
			ColumnScroll
		End Enum

	    Dim SortField As String

#End Region

#Region " Public Events "

		Public Event DirChanged As DirChangedEventHandler

		Protected Overridable Sub OnDirChanged(ByVal e As DirChangedEventArgs)
			Session("dt") = Nothing
			RaiseEvent DirChanged(Me, e)
		End Sub

		Public Event FileClicked As FileClickedEventHandler

		Protected Overridable Sub OnFileClicked(ByVal e As FileClickedEventArgs)
			RaiseEvent FileClicked(Me, e)
		End Sub

		Public Event CheckClicked As CheckClickedEventHandler

		Protected Overridable Sub OnCheckClicked(ByVal e As CheckClickedEventArgs)
			RaiseEvent CheckClicked(Me, e)
		End Sub

#End Region

#Region " Public Properties "



		Public Property Root() As String
			Get
				Session("RootDir") = Me.ViewState("RootDir")
				Return Me.ViewState("RootDir")
			End Get
			Set(ByVal Value As String)
				If Me.ViewState("RootDir") <> Value Then
                    Dim _Root As String = ""
					If Value.IndexOf(":") <> -1 Then
						_Root = Value
					End If
					If Right(_Root, 1) = "\" Then
						_Root = Left(_Root, _Root.Length - 1)
					End If
					Me.ViewState("RootDir") = _Root
					Session("RootDir") = _Root
					Session("dt") = Nothing
					BindData()
					Dim e As New DirChangedEventArgs(_Root, RelativeDir)
					OnDirChanged(e)
				End If
			End Set
		End Property

		Public Property RelativeDir() As String
			Get
			    Session("RelativeDir") = Me.ViewState("RelativeDir")
				Return Me.ViewState("RelativeDir")
			End Get
			Set(ByVal Value As String)
				If Me.ViewState("RelativeDir") <> Value Then
					Me.ViewState("RelativeDir") = Value
					Session("RelativeDir") = Value
					Session("dt") = Nothing
					BindData()
					Dim e As New DirChangedEventArgs(Root, Value)
					OnDirChanged(e)
				End If
			End Set
		End Property

		Public Property AllowRecursiveDelete() As Boolean
			Get
				Return Me.ViewState("RecursiveDelete")
			End Get
			Set(ByVal Value As Boolean)
				Me.ViewState("RecursiveDelete") = Value
			End Set
		End Property

		Public Property RoundTripOnCheck() As Boolean
			Get
				Return Me.ViewState("RoundTripOnCheck")
			End Get
			Set(ByVal Value As Boolean)
				Me.ViewState("RoundTripOnCheck") = Value
			End Set
		End Property
#End Region

#Region " Public Methods "
		Public Sub NavigateParentDir()
			Dim RelDir As String = RelativeDir
			If RelDir <> "" Then
				RelativeDir = Left(RelDir, InStrRev(RelDir, "\") - 1)
			End If
		End Sub

		Public Sub Refresh()
			Session("dt") = Nothing
			BindData()
		End Sub

		Public Sub DeleteSelected()
		
			Dim StrFolder as String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
            If Not (Request.Params("hostpage") Is Nothing) Then
                StrFolder = Request.MapPath(glbSiteDirectory)
                Else
                StrFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If

			Try
				Dim grdItem As DataGridItem
				For Each grdItem In grdFileFolders.Items
					Dim chkItem As Tag.WebControls.CheckBoxItem = GetCheckSelect(grdItem)
					If chkItem.Checked = True Then
						Dim FName As String = GetNameLinkButton(grdItem).Text()
						If IsDirectory(grdItem) Then
							Directory.Delete(Root & RelativeDir & "\" & FName, AllowRecursiveDelete)
						Else
							File.Delete(Root & RelativeDir & "\" & FName)
							If StrFolder = Root & RelativeDir & "\" then
 					        	If Not (Request.Params("hostpage") Is Nothing) Then
	 				        	objAdmin.DeleteFile(FName)
                            	Else
                				objAdmin.DeleteFile(FName, _portalSettings.PortalId)
                				End If
							end if
						End If
					End If
				Next
			Catch e As Exception
				lblErr.Text = e.Message
				lblErr.Visible = True
			End Try
			'Refresh Grid
            ' update the directory size
            objAdmin.AddDirectory(StrFolder, objAdmin.GetFolderSizeRecursive(StrFolder))
            Session("dt") = Nothing
            BindData()
        End Sub

		Public Sub DownloadFile()
			Try
				Dim grdItem As DataGridItem
				For Each grdItem In grdFileFolders.Items
					Dim chkItem As Tag.WebControls.CheckBoxItem = GetCheckSelect(grdItem)
					If chkItem.Checked = True Then
						Dim FName As String = GetNameLinkButton(grdItem).Text()
						If IsFile(grdItem) Then
                            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                            Dim objSecurity As New PortalSecurity()
                            Dim crypto As String = Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), Root & RelativeDir & "\" & FName))
                            Response.Redirect(ResolveUrl("TAGFileDownload.aspx") & "?File=" & crypto & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId))
							chkItem.Checked = False
							Exit For
						End If
					End If
				Next
			Catch e As Exception
				lblErr.Text = e.Message
				lblErr.Visible = True
			End Try
		End Sub

		Public Function SelectedCount() As Integer
			Dim i As Integer
			Dim grdItem As DataGridItem
			For Each grdItem In grdFileFolders.Items
				If GetCheckSelect(grdItem).Checked Then
					i += 1
				End If
			Next
			Return i
		End Function

		Public Sub EditFileFolderName()
			Try
				Dim grdItem As DataGridItem
				For Each grdItem In grdFileFolders.Items
					If GetCheckSelect(grdItem).Checked = True Then
						grdFileFolders.EditItemIndex = grdItem.ItemIndex
						BindData()
						Exit For
					End If
				Next
			Catch e As Exception
				lblErr.Text = e.Message
				lblErr.Visible = True
			End Try
		End Sub

		Public Sub CreateNewFolder(ByVal NewDirBase As String)
			Try
				Dim di As New DirectoryInfo(Root & RelativeDir)
				
				If NewDirBase = "" then
				NewDirBase = GetLanguage("New_Directory")
				end if
				Dim i As Integer = 0
				Dim NewDir As String
				NewDir = NewDirBase
				While DirectoryExists(di, NewDir)
					NewDir = String.Format("{0} ({1})", NewDirBase, i)
				End While
				di.CreateSubdirectory(NewDir)
				Session("dt") = Nothing
				BindData()
			Catch e As Exception
				lblErr.Text = e.Message
				lblErr.Visible = True
			End Try
		End Sub
#End Region

#Region " Constituent Events "
		Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			'Put user code to initialize the page here


        If Not IsPostBack Then
		Session("sort") = "Type"
        SortField = "Type"
       			If Root = "" Then
				if Session("FCKeditor:UserFilesPath") <> "" then
				Root = Request.MapPath(Session("FCKeditor:UserFilesPath"))
				Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + Session("FCKeditor:UserFilesPath")
				else
		            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        		    If Not (Request.Params("hostpage") Is Nothing) Then
                	Root = Request.MapPath(glbSiteDirectory)
					Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + glbSiteDirectory
                	Else
                	Root = Request.MapPath(_portalSettings.UploadDirectory)
					Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + _portalSettings.UploadDirectory
                	End If
				End If
			End If
        End If
	SortField = Session("sort")	
		End Sub

		
	
		
		

		Private Sub grdFileFolders_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFileFolders.ItemCommand
			Select Case e.CommandName
				Case "CheckClick"
					Dim lnk As LinkButton = GetNameLinkButton(e.Item)
					If not lnk is nothing then
					Dim evt As New CheckClickedEventArgs(Root & RelativeDir & "\" & lnk.Text(), CType(e.CommandSource, Tag.WebControls.CheckBoxItem), SelectedCount)
					OnCheckClicked(evt)
					end if
				Case "Select"
					' If this is a directory then goto new directory
					If IsDirectory(e.Item) Then
						RelativeDir += "\" & CType(e.CommandSource, LinkButton).Text()
					Else					  ' If this is a file - will open file
						If InStr(1, context.Request.Url.ToString.ToLower, "/filemanager.aspx") <> 0 Then
						Dim lnk As LinkButton = GetNameLinkButton(e.Item)
 						If not lnk is nothing then
						Dim incScript As String = String.Format("<script Language=""javascript"">window.top.opener.SetUrl('{0}'); window.top.close() ; window.top.opener.focus() ;</script>", GetFullUrlName(lnk.Text))
                                Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "OpenFileManager", incScript)
					end if
						else
						Dim evt As New FileClickedEventArgs(Root, RelativeDir, CType(e.CommandSource, LinkButton).Text())
						OnFileClicked(evt)
						end if
					End If
				Case "EditOK"
					RenameFileFolder(e.Item.ItemIndex, e.CommandArgument, GetRenameTextBox(e.Item).Text)
					grdFileFolders.EditItemIndex = -1
					Session("dt") = Nothing
					BindData()
				Case "EditCancel"
					grdFileFolders.EditItemIndex = -1
					BindData()
			End Select
		End Sub

		Private Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
			Dim grdItem As DataGridItem
			For Each grdItem In grdFileFolders.Items
				CType(grdItem.Cells(0).Controls(0), Tag.WebControls.CheckBoxItem).Checked = chkSelectAll.Checked
			Next
		End Sub
#End Region

#Region " Private Methods "
		Private Sub BindData()
  		grdFileFolders.DataSource = XFillData()
		grdFileFolders.DataBind()
        End Sub

	
		
		
Private Function XFillData() As ICollection

	Dim CurrentDir As System.IO.DirectoryInfo
	Dim ChildDirs As System.IO.DirectoryInfo()
	Dim ChildDir As System.IO.DirectoryInfo
	Dim ChildFiles As System.IO.FileInfo()
	Dim ChildFile As System.IO.FileInfo
	Dim dt As DataTable
	Dim dr As DataRow
    Dim extIndex As Integer
	Dim TempImageUrl As String
	Dim TempSize As Long
	' get it from memory if there
	If not Session("dt") is nothing then
	dt = Session("dt")
	else
	Try
	If RelativeDir = "" Then
	   CurrentDir = New DirectoryInfo(Root)
	Else
	   CurrentDir = New DirectoryInfo(Root & RelativeDir)
	End If
	ChildDirs = CurrentDir.GetDirectories
	ChildFiles = CurrentDir.GetFiles

        dt = New DataTable
        dt.Columns.Add(New DataColumn("Icon", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))
        dt.Columns.Add(New DataColumn("Type", GetType(String)))
        dt.Columns.Add(New DataColumn("Size", GetType(String)))
        dt.Columns.Add(New DataColumn("CreateDate", GetType(DateTime)))
        dt.Columns.Add(new DataColumn("ModifyDate", GetType(DateTime)))
        dt.Columns.Add(New DataColumn("RowType", GetType(String)))
		dt.Columns.Add(New DataColumn("RawSize", GetType(Long)))
		'Icon, Name, Type, Size, CreateDate, ModifyDate, RowType		
				For Each ChildDir In ChildDirs
				    TempSize = GetDirSize(ChildDir)
				    dr = dt.NewRow()
                        dr("Icon") = "<img src=""" & glbPath & "Admin/AdvFileManager/images/closedfolder.gif"" alt=""dir"" border=""0"" style=""height:16px;width:16px;"">"
					dr("Name") = ChildDir.Name
					dr("Type") = ""
					dr("Size") = FormatSize(TempSize)
					dr("CreateDate") = ChildDir.CreationTime
					dr("ModifyDate") = ChildDir.LastWriteTime
					dr("RowType") = "Dir"
					dr("RawSize") = TempSize
					dt.Rows.Add(dr)
				Next
				For Each ChildFile In ChildFiles
					dr = dt.NewRow()
					extIndex = Array.IndexOf(extensions, ChildFile.Extension.ToLower())
					If extIndex > -1 Then
                            TempImageUrl = "/images/" & CType(ChildFile.Extension, String).Substring(1) & ".gif"
                        Else
                            TempImageUrl = "/images/unknown.gif"
					End If
                        dr("Icon") = "<img src=""" & glbPath & "Admin/AdvFileManager" & TempImageUrl & """ Title=""" & ChildFile.Extension & """ alt=""" & ChildFile.Extension & """ border=""0"" style=""height:16px;width:16px;"">"
					dr("Name") = ChildFile.Name
					dr("Type") = ChildFile.Extension
					dr("Size") = FormatSize(ChildFile.Length)
					dr("CreateDate") = ChildFile.CreationTime
					dr("ModifyDate") = ChildFile.LastWriteTime
					dr("RowType") = "File"
					dr("RawSize") = ChildFile.Length
					dt.Rows.Add(dr)
				Next
			Catch e As Exception
				lblErr.Text() = e.Message
				lblErr.Visible = True
			End Try
		End if
        'return a DataView to the DataTable
        Dim dv as DataView = New DataView(dt)
        dv.Sort = SortField
		Session("dt") = dt
        XFillData = dv
				
End Function
		
		
		
		Private Function FormatSize(ByVal Size As Long) As String
			If Size < 1024 Then
				Return String.Format("{0:N0} B", Size)
			ElseIf (Size < 1048576) Then
				Return String.Format("{0:N2} KB", Size / 1024)
			Else
				Return String.Format("{0:N2} MB", Size / (1048576))
			End If
		End Function

		Private Function GetDirSize(ByVal CurDir As DirectoryInfo) As Long
			Dim dirSize As Long
			Dim theFile As FileInfo

			Dim subDir As DirectoryInfo
			For Each subDir In CurDir.GetDirectories
				dirSize += GetDirSize(subDir)
			Next
			For Each theFile In CurDir.GetFiles
				dirSize += theFile.Length
			Next
			Return dirSize
		End Function

		Private Function IsDirectory(ByVal Item As DataGridItem) As Boolean
			If CType(Item.Cells(GridColumn.ColumnType).Controls(1), Label).Text = "" Then
				Return True
			Else
				Return False
			End If
		End Function

		Private Function IsFile(ByVal Item As DataGridItem) As Boolean
			If IsDirectory(Item) Then
				Return False
			Else
				Return True
			End If
		End Function

		Private Function GetNameLinkButton(ByVal item As DataGridItem) As LinkButton
			Return CType(item.FindControl("lnkNameColumn"), LinkButton)
		End Function


		Private Function GetCheckSelect(ByVal item As DataGridItem) As Tag.WebControls.CheckBoxItem
			Return CType(item.Cells(GridColumn.ColumnCheck).Controls(0), Tag.WebControls.CheckBoxItem)
		End Function

		Private Function GetImageIcon(ByVal item As DataGridItem) As Image
			Return CType(item.Cells(GridColumn.ColumnIcon).Controls(1), Image)
		End Function

		Private Function GetImageOK(ByVal item As DataGridItem) As Tag.WebControls.RolloverImageButton
			Return CType(item.FindControl("imgEditOK"), Tag.WebControls.RolloverImageButton)
		End Function

		Private Function GetRenameTextBox(ByVal item As DataGridItem) As TextBox
			Return CType(item.FindControl("txtRename"), TextBox)
		End Function

		Private Sub RenameFileFolder(ByVal ItemIndex As Integer, ByVal oldName As String, ByVal newName As String)
			Try
				Dim strExtension As String
				Dim grdItem As DataGridItem = grdFileFolders.Items(ItemIndex)
				If IsDirectory(grdItem) Then
					Dim di As DirectoryInfo = New DirectoryInfo(GetFullName(oldName))
					di.MoveTo(GetFullName(newName))
				Else
	            strExtension = ""
	            If InStr(1, newName, ".") Then
    	                 strExtension = Mid(newName, InStrRev(newName, ".") + 1).ToLower
        	    End If
	            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
	            If strExtension <> "" and InStr(1, "," & portalSettings.GetHostSettings("FileExtensions").ToString, "," & strExtension) <> 0 Or Not (Request.Params("hostpage") Is Nothing) Then
					Dim fi As FileInfo = New FileInfo(GetFullName(oldName))
					fi.MoveTo(GetFullName(newName))
				else
				    lblErr.Text = replace(GetLanguage("FileExtNotAllowed"), "{fileext}", newName)
					lblErr.Text = replace(lblErr.Text, "{allowedext}", " ( *." & Replace(portalSettings.GetHostSettings("FileExtensions").ToString, ",", ", *.") & " ).")
					lblErr.Visible = True
				end if
				End If
			Catch e As Exception
				lblErr.Text = e.Message
				lblErr.Visible = True
			End Try
		End Sub

		Private Function GetFullName(ByVal Name As String) As String
			Return Root & RelativeDir & "\" & Name
		End Function

		Function GetFullUrlName(ByVal Name As String) As String
			If Session("RootUrl") = "" then
				if Session("FCKeditor:UserFilesPath") <> "" then
				Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + Session("FCKeditor:UserFilesPath")
				else
		            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        		    If Not (Request.Params("hostpage") Is Nothing) Then
					Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + glbSiteDirectory
                	Else
 					Session("RootUrl") = Request.ServerVariables("HTTP_HOST") + _portalSettings.UploadDirectory
                	End If
				End If
			end if

			Dim tempstring as string
			TempString = Session("RootUrl") & Replace(RelativeDir, "\", "/") & "/" & Name
			TempString = replace(Tempstring, "//", "/")
			return tempstring
		End Function
		
		
		Private Function DirectoryExists(ByVal DirInfo As DirectoryInfo, ByVal DirName As String) As Boolean
			Dim di As DirectoryInfo
			For Each di In DirInfo.GetDirectories
				If UCase(di.Name) = UCase(DirName) Then
					Return True
				End If
			Next
			Return False
		End Function

		Sub SortFileFolders(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs) 

        SortField = e.SortExpression

		BindData()


		End Sub

		
		Private Sub SortFileType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSortTypeup.Click, chkSortTypedown.Click, chkSortNameup.Click, chkSortNamedown.Click, chkSortSizeup.Click, chkSortSizeDown.Click

         ' Sort property with the name of the field to sort by.
		 ' SORT_DESC and SORT_ASC are constants with the following string values.
         ' SORT_DESC = “Desc”
         ' SORT_ASC  = “Asc”
	    Session("sort") =  "RowType ASC, " & CType(sender, LinkButton).CommandName
		SortField = Session("sort")
		BindData()
		End Sub
#End Region
	End Class
End Namespace