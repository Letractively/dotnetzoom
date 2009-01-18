<%@ Control Language="VB"%>
<%@ Import Namespace="DotNetZoom" %>

<script runat="server">

	Public TPL_1 As New System.Text.StringBuilder
	Public XtigraMenuString as String
	
Private Function GetTPL() As String

       Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
       Dim strPageHTML As String = ""
	   Dim SkinFileName As String = ""

	   If IsAdminTab() or Request.Params("edit") <> "" then
						SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/tigraedit.js")
						strPageHTML = Context.Cache(SkinFileName)
						If strPageHTML = nothing then
							If Not System.IO.File.Exists(SkinFileName) then
							SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
 								If Not System.IO.File.Exists(SkinFileName) then
                        SkinFileName = Request.MapPath("/javascript/menu_tpl1.js")
								end if
							end if
						end if
						strPageHTML = Context.Cache(SkinFileName)
							' if page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As System.IO.StreamReader
            				objStreamReader = System.IO.File.OpenText(SkinFileName)
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
							If Not System.IO.File.Exists(SkinFileName) then
							SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
								If Not System.IO.File.Exists(SkinFileName) then
                        SkinFileName = Request.MapPath("/javascript/menu_tpl1.js")
								end if
							end if
						strPageHTML = Context.Cache(SkinFileName)
							'  page not in memory get it
							If strPageHTML Is Nothing Then
							Dim objStreamReader As System.IO.StreamReader
            				objStreamReader = System.IO.File.OpenText(SkinFileName)
            				strPageHTML = objStreamReader.ReadToEnd
            				objStreamReader.Close()
							Dim dep As New System.Web.Caching.CacheDependency(SkinFileName)
	      					Cache.Insert(SkinFileName, strPageHTML, dep)
							end if
					end if
			end if
Return strPageHTML
End Function
	
		Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		vertical.Attributes.Add("onmouseover", ReturnToolTip(getlanguage("tigra_help")))
		horizontal.Attributes.Add("onmouseover", ReturnToolTip(getlanguage("tigra_help1")))
		Tester.Attributes.Add("onmouseover", ReturnToolTip(getlanguage("tigra_help2")))
		sauvegarder.Attributes.Add("onmouseover", ReturnToolTip(getlanguage("tigra_help3")))
		retour.Attributes.Add("onmouseover", ReturnToolTip(getlanguage("tigra_return")))
		vertical.Text = getlanguage("tigra_vertical")
		horizontal.Text = getlanguage("tigra_horizontal")
		Tester.Text = getlanguage("tigra_tester")
		sauvegarder.Text = getlanguage("enregistrer")
		retour.Text = getlanguage("return")
 


        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        If PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) = False Then
              Response.Redirect("~/default.aspx?tabid=" & _portalSettings.ActiveTab.TabId & "&def=Edit Access Denied", True)
        End If

		
		If Not Page.IsPostBack then
		
        If _portalSettings.LogoFile <> "" Then
		Dim objAdmin As New AdminDB()
		Dim dr As System.Data.SqlClient.SqlDataReader
		dr = objAdmin.GetSingleFile(_portalSettings.LogoFile,_portalSettings.PortalId)
		If dr.Read Then
		if isnumeric(dr("Width").ToString) then
		Centrer.Text = dr("Width").ToString
		else
		Centrer.Text = "0"
		end if
		if isnumeric(dr("Height").ToString) then
		Block_Top.Text = dr("Height").ToString
		else
		Block_Top.Text = "0"
		end if
		end if
		dr.Close()
        Else
		Centrer.Text = "0"
		Block_Top.Text = "0"
        End If
    
	
		Open_Delay.Text = "20"
		Close_Delay.Text = "200"
		Block_Left.Text = "0"
		Top.Text = "0"
		Left.Text = "125"
		Height.Text = "24"
		Width.Text = "125"
		
		
		Block_Top2.Text = "25"
		Block_Left2.Text = "0"
		Top2.Text = "25"
		Left2.Text = "0"
		Height2.Text = "24"
		Width2.Text = "190"

		Block_Top3.Text = "0"
		Block_Left3.Text = "180"
		Top3.Text = "25"
		Left3.Text = "0"
		Height3.Text = "24"
		Width3.Text = "190"

		ReadTplFile()

		
		End If
		
		If Not IsNumeric(Centrer.Text) then
		Centrer.Text = "0"
		end if

		If Not IsNumeric(Open_Delay.Text) then
		Open_Delay.Text = "20"
		end if
		If Not IsNumeric(Close_Delay.Text) then
		Close_Delay.Text = "220"
		end if

		
		
		If Not IsNumeric(Block_Top.Text) then
		Block_Top.Text = "0"
		end if
		If Not IsNumeric(Block_Left.Text) then
		Block_Left.Text = "0"
		end if

		
		If Not IsNumeric(Top.Text) then
		Top.Text = "0"
		end if
		If Not IsNumeric(Left.Text) then
		Left.Text = "0"
		end if
		
		If Not IsNumeric(Height.Text) then
		Height.Text = "24"
		else
		if Double.Parse(Height.Text) < 10 then
		Height.Text = "10"
		else
		if Double.Parse(Height.Text) > 60 then
		Height.Text = "60"
		end if
		end if
		End if
		
		If Not IsNumeric(Width.Text) then
		Width.Text = "90"
		else
		if Double.Parse(Width.Text) < 30 then
		Width.Text = "30"
		else
		if Double.Parse(Width.Text) > 300 then
		Width.Text = "300"
		end if
		end if
		end if

		If Not IsNumeric(Block_Top2.Text) then
		Block_Top2.Text = "0"
		end if
		If Not IsNumeric(Block_Left2.Text) then
		Block_Left2.Text = "0"
		end if

		
		If Not IsNumeric(Top2.Text) then
		Top2.Text = "0"
		end if
		If Not IsNumeric(Left2.Text) then
		Left2.Text = "0"
		end if
		
		If Not IsNumeric(Height2.Text) then
		Height2.Text = "24"
		else
		if Double.Parse(Height2.Text) < 10 then
		Height2.Text = "10"
		else
		if Double.Parse(Height2.Text) > 60 then
		Height2.Text = "60"
		end if
		end if
		End if
		
		If Not IsNumeric(Width2.Text) then
		Width2.Text = "90"
		else
		if Double.Parse(Width2.Text) < 30 then
		Width2.Text = "30"
		else
		if Double.Parse(Width2.Text) > 300 then
		Width2.Text = "300"
		end if
		end if
		end if
		
		
		
		If Not IsNumeric(Block_Top3.Text) then
		Block_Top3.Text = "0"
		end if
		If Not IsNumeric(Block_Left3.Text) then
		Block_Left3.Text = "0"
		end if

		
		If Not IsNumeric(Top3.Text) then
		Top3.Text = "0"
		end if
		If Not IsNumeric(Left3.Text) then
		Left3.Text = "0"
		end if
		
		If Not IsNumeric(Height3.Text) then
		Height3.Text = "24"
		else
		if Double.Parse(Height3.Text) < 10 then
		Height3.Text = "10"
		else
		if Double.Parse(Height3.Text) > 60 then
		Height3.Text = "60"
		end if
		end if
		End if
		
		If Not IsNumeric(Width3.Text) then
		Width3.Text = "90"
		else
		if Double.Parse(Width3.Text) < 30 then
		Width3.Text = "30"
		else
		if Double.Parse(Width3.Text) > 300 then
		Width3.Text = "300"
		end if
		end if
		end if
		
		
		
		If Not Page.IsPostBack then
	    CreateMenu()
		CreateTigraMenu()
		End IF
		
        End Sub


Public Sub CreateMenu()
		TPL_1.Append(vbCrLf )
		
		
		TPL_1.Append("var TempY = " & Block_Top.Text & ";" & vbCrLf )
		TPL_1.Append("var TempX = " & Block_Left.Text & ";" & vbCrLf )
		TPL_1.Append( vbCrLf )
            
        If Centrer.Text <> "0" Then
            TPL_1.Append("function windowWidth(){" & vbCrLf)
            TPL_1.Append("    if (window.innerWidth){" & vbCrLf)
            TPL_1.Append("        if (document.body.offsetWidth){" & vbCrLf)
            TPL_1.Append("            if (window.innerWidth!=document.body.offsetWidth)" & vbCrLf)
            TPL_1.Append("                return document.body.offsetWidth;" & vbCrLf)
            TPL_1.Append("            }" & vbCrLf)
            TPL_1.Append("       return (window.innerWidth);                     // Mozilla" & vbCrLf)
            TPL_1.Append("    }" & vbCrLf)
            TPL_1.Append("    if (document.documentElement.clientWidth)" & vbCrLf)
            TPL_1.Append("        return document.documentElement.clientWidth;    // IE6" & vbCrLf)
            TPL_1.Append("    if (document.body.clientWidth)" & vbCrLf)
            TPL_1.Append("        return document.body.clientWidth;               // IE DHTML-compliant any other" & vbCrLf)
            TPL_1.Append("}" & vbCrLf)
            TPL_1.Append("var winW = windowWidth();" & vbCrLf)
            TPL_1.Append("TempX = (winW-" & Centrer.Text & ")/2;" & vbCrLf)
            TPL_1.Append(vbCrLf)
        End If
        
        
        TPL_1.Append("var MENU_POS1 = [")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("{'height': " & Height.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'width': " & Width.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_top': TempY ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_left': TempX ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'top': " & Top.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'left': " & Left.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'hide_delay': " & Close_Delay.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'expd_delay': " & Open_Delay.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'css' : {")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'outer' : ['m0l0oout', 'm0l0oover' , 'menu0downo'],")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'inner' : ['m0l0iout', 'm0l0iover' ,'m0l0odown']")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("}")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("},")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("{")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'height': " & Height2.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'width': " & Width2.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_top': " & Block_Top2.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_left':  " & Block_Left2.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'top': " & Top2.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'left': " & Left2.Text)
        TPL_1.Append(vbCrLf)
        TPL_1.Append("},")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("{")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'height': " & Height3.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'width': " & Width3.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_top': " & Block_Top3.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'block_left': " & Block_Left3.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'top': " & Top3.Text & " ,")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("'left': " & Left3.Text)
        TPL_1.Append(vbCrLf)
        TPL_1.Append("}")
        TPL_1.Append(vbCrLf)
        TPL_1.Append("]")
        TPL_1.Append(vbCrLf)
    End Sub
	
    Public Sub Tester_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CreateMenu()
        CreateTigraMenu()
    End Sub

    Public Sub Vertical_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
        If _portalSettings.LogoFile <> "" Then
            Dim objAdmin As New AdminDB()
            Dim dr As System.Data.SqlClient.SqlDataReader
            dr = objAdmin.GetSingleFile(_portalSettings.LogoFile, _portalSettings.PortalId)
            If dr.Read Then
                If IsNumeric(dr("Height").ToString) Then
                    Block_Top.Text = Double.Parse(dr("Height").ToString) + 100
                Else
                    Block_Top.Text = "0"
                End If
            End If
            dr.Close()
        Else
            Block_Top.Text = "0"
        End If
	
        Centrer.Text = "0"
        Top.Text = Double.Parse(Height.Text) + 1
        Left.Text = "0"
		
        Block_Top2.Text = "0"
        Block_Left2.Text = Double.Parse(Width.Text) + 2
        Top2.Text = Double.Parse(Height.Text) + 1
        Left2.Text = "0"


        Block_Left3.Text = Double.Parse(Width2.Text) + 2
        Top3.Text = Top2.Text
        CreateMenu()
        CreateTigraMenu()


    End Sub

    Public Sub Horizontal_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
        If _portalSettings.LogoFile <> "" Then
            Dim objAdmin As New AdminDB()
            Dim dr As System.Data.SqlClient.SqlDataReader
            dr = objAdmin.GetSingleFile(_portalSettings.LogoFile, _portalSettings.PortalId)
            If dr.Read Then
                If IsNumeric(dr("Height").ToString) Then
                    Block_Top.Text = Double.Parse(dr("Height").ToString)
                Else
                    Block_Top.Text = "0"
                End If
            End If
            dr.Close()
        Else
            Block_Top.Text = "0"
        End If
	
        Centrer.Text = "0"
        Top.Text = "0"
        Left.Text = Width.Text
		
        Block_Top2.Text = Double.Parse(Height.Text) + 1
        Block_Left2.Text = "0"
        Top2.Text = Height.Text
        Left2.Text = "0"


        Block_Left3.Text = Double.Parse(Width2.Text) - 10
        Top3.Text = Top2.Text

        CreateMenu()
        CreateTigraMenu()


    End Sub

    Public Sub Retour_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Context.Response.Redirect(Replace(Request.RawUrl.ToString.ToLower, "editmenu=1", "menu=ok"), True)
    End Sub
    Public Sub Sauvegarder_OnClick(ByVal sender As Object, ByVal e As EventArgs)


        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Dim strPageHTML As String = ""
        Dim SkinFileName As String = ""

        If IsAdminTab() Or Request.Params("edit") <> "" Then
            SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/tigraedit.js")
        Else
            ' get standard page
            If _portalSettings.ActiveTab.Skin <> "" Then
                Dim TempMenuFile As String
                TempMenuFile = Replace(_portalSettings.ActiveTab.Skin, ".skin", "_menu.js")
                SkinFileName = Request.MapPath(_portalSettings.UploadDirectory & "skin/" & TempMenuFile)
            Else
                SkinFileName = Server.MapPath(_portalSettings.UploadDirectory & "skin/menu_tpl1.js")
            End If
        End If
        If Not System.IO.Directory.Exists(Server.MapPath(_portalSettings.UploadDirectory & "skin/")) Then
            System.IO.Directory.CreateDirectory(Server.MapPath(_portalSettings.UploadDirectory & "skin/"))
        End If
        ' write file
        Dim objStream As System.IO.StreamWriter
        objStream = System.IO.File.CreateText(SkinFileName)
        CreateMenu()
        objStream.WriteLine(TPL_1.ToString())
        objStream.Close()
        Context.Response.Redirect(Replace(Request.RawUrl.ToString.ToLower, "editmenu=1", "menu=ok"), True)
    End Sub



    Public Function ParamToFind(ByVal InWhat As String, ByVal WhatTo As String, Optional ByVal StrDefault As String = "", Optional ByVal Delim As String = ",") As String
			
        Dim StartPoint As Integer
        Dim EndPoint As Integer
        StartPoint = InWhat.IndexOf(WhatTo) + WhatTo.Length
        If StartPoint > WhatTo.Length Then
            EndPoint = InWhat.IndexOf(Delim, StartPoint)
            If EndPoint < StartPoint Then
                EndPoint = InWhat.Length
            End If
            Return InWhat.Substring(StartPoint, EndPoint - StartPoint)
        Else
            Return StrDefault
        End If


    End Function


    Public Sub ReadTplFile()

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Dim StartPoint As Integer
        Dim EndPoint As Integer
        Dim TempString As String
        Dim ReadString As String = GetTPL()

        Block_Top.Text = Trim(ParamToFind(ReadString, "var TempY =", "50", ";"))
        Block_Left.Text = Trim(ParamToFind(ReadString, "var TempX =", "50", ";"))
        Centrer.Text = ParamToFind(ReadString, "winW-", "", ")")
 
        StartPoint = ReadString.IndexOf("MENU_POS1")
        EndPoint = StartPoint
        If StartPoint > 0 Then
            EndPoint = ReadString.IndexOf("}", StartPoint)
            TempString = LCase(ReadString.Substring(StartPoint, EndPoint - StartPoint))
            TempString = TempString.Replace(" ", "")
            TempString = TempString.Replace(vbCrLf, "")
			
            If Not IsNumeric(Block_Top.Text) Then
                Block_Top.Text = ParamToFind(TempString, "'block_top':")
            End If
            If Not IsNumeric(Block_Top.Text) Then
                Block_Top.Text = "0"
            End If

			
            Open_Delay.Text = ParamToFind(TempString, "'expd_delay':")
            Close_Delay.Text = ParamToFind(TempString, "'hide_delay':")

            If Not IsNumeric(Block_Left.Text) Then
                Block_Left.Text = ParamToFind(TempString, "'block_left':")
            End If
            If Not IsNumeric(Block_Left.Text) Then
                Block_Left.Text = "0"
            End If

            Top.Text = ParamToFind(TempString, "'top':")
            Left.Text = ParamToFind(TempString, "'left':")
            Height.Text = ParamToFind(TempString, "'height':")
            Width.Text = ParamToFind(TempString, "'width':")

			
        End If
			
        StartPoint = ReadString.IndexOf("{", EndPoint)
        EndPoint = ReadString.IndexOf("}", StartPoint)

        TempString = ReadString.Substring(StartPoint, EndPoint - StartPoint)
        TempString = TempString.Replace(" ", "")
        TempString = TempString.Replace(vbCrLf, "")

			
        Block_Top2.Text = ParamToFind(TempString, "'block_top':", Block_Top.Text)
        Block_Left2.Text = ParamToFind(TempString, "'block_left':", Block_Left.Text)
        Top2.Text = ParamToFind(TempString, "'top':", Top.Text)
        Left2.Text = ParamToFind(TempString, "'left':", Left.Text)
        Height2.Text = ParamToFind(TempString, "'height':", Height.Text)
        Width2.Text = ParamToFind(TempString, "'width':", Width.Text)

			
        StartPoint = ReadString.IndexOf("{", EndPoint)
        EndPoint = ReadString.IndexOf("}", StartPoint)

        TempString = ReadString.Substring(StartPoint, EndPoint - StartPoint)
        TempString = TempString.Replace(" ", "")
        TempString = TempString.Replace(vbCrLf, "")

        Block_Top3.Text = ParamToFind(TempString, "'block_top':", Block_Top2.Text)
        Block_Left3.Text = ParamToFind(TempString, "'block_left':", Block_Left2.Text)
        Top3.Text = ParamToFind(TempString, "'top':", Top2.Text)
        Left3.Text = ParamToFind(TempString, "'left':", Left2.Text)
        Height3.Text = ParamToFind(TempString, "'height':", Height2.Text)
        Width3.Text = ParamToFind(TempString, "'width':", Width2.Text)

    End Sub

    Public Sub CreateTigraMenu()

        ' Obtain PortalSettings from Current Context
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        ' Build list of tabs to be shown to user
        Dim authorizedTabs As New ArrayList()
        Dim addedTabs As Integer = 0
        Dim LastLevel As Integer
        Dim DifLevel As Integer
        Dim TabLevel As Integer
        Dim LastStop As Boolean
        DifLevel = 0
        LastLevel = 0

        Dim DesktopTabs As ArrayList = PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N"))
        Dim i As Integer
        For i = 0 To DesktopTabs.Count - 1
            Dim tab As TabStripDetails = CType(DesktopTabs(i), TabStripDetails)
            Dim strTest As String = tab.TabName
            If tab.IsVisible Then
                If PortalSecurity.IsInRoles(tab.AuthorizedRoles) = True Then
                    If DifLevel = tab.Level Then
                        tab.Level = LastLevel
                    End If
                    DifLevel = tab.Level
                    If tab.Level > (LastLevel) Then
                        tab.Level = (LastLevel + 1)
                    End If

                    LastLevel = tab.Level
						
						
                    authorizedTabs.Add(tab)
                    addedTabs += 1
						
                End If
            End If

        Next i


        Dim objTab As TabStripDetails
 			
        Dim TempString As StringBuilder = New System.Text.StringBuilder()
        Dim TempTabName As String
			 
        Dim NotFirstTimeAround As Boolean
        TempString.Append(ControlChars.Cr & "var MENU_ITEMS = [ " & ControlChars.Cr)
        LastLevel = 0
		
			
        TabLevel = 0
        NotFirstTimeAround = False
        LastStop = False
			
        For Each objTab In authorizedTabs

            Try
			
                If NotFirstTimeAround Then
                    If objTab.Level <= LastLevel Then
                        For i = objTab.Level To LastLevel
                            If i <> objTab.Level Then TabLevel = TabLevel - 1
                            TempString.Append(" ], " & ControlChars.Cr)
                        Next i
                    Else
                        TempString.Append(" , ")
                        TabLevel = TabLevel + 1
                    End If
                End If
			
			
                TempString.Append("[")


				
                If objTab.IconFile <> "" Then
                    TempString.Append("Ti('" & objTab.IconFile & "') + ")
                End If

				
                TempTabName = objTab.TabName
                If _portalSettings.ActiveTab.TabId = objTab.TabId Then
                    ' HighLight menu if active tab
                    TempTabName = "<span class=""activetab"">" & TempTabName & "<\/span>"
                End If
                TempTabName = TempTabName.Replace("'", "\'")
                TempString.Append("'" & TempTabName)
                If objTab.DisableLink Then
                    TempString.Append("' , '' , {'sb' : '" & TempTabName & "'} ")
                Else
                    TempString.Append("' , '' , {'sb' : '" & TempTabName & "'} ")
                End If
                NotFirstTimeAround = True
                LastLevel = objTab.Level
            Catch
                ' throws exception if the parent tab has not been loaded ( may be related to user role security not allowing access to a parent tab )
            End Try
        Next
		   
        For i = 0 To TabLevel
            TempString.Append(" ]," & ControlChars.Cr)
        Next i
			   
        TempString.Append(" ];" & ControlChars.Cr)
        TempString.Append("function Ti (text) { return '<img src=""" & _portalSettings.UploadDirectory & "' + text + '"" border=""0"" alt=""""> ';}" & ControlChars.Cr)
        TempString.Append("function Ta (text) { return '<img src=""" & "/images/" & "' + text + '"" border=""0"" alt=""""> ';}" & ControlChars.Cr)
        XtigraMenuString = TempString.ToString()
    End Sub

</script>
<script language="javascript" type="text/javascript" src="/javascript/wz_dragdrop.js"></script>
<script language="JavaScript" type="text/javascript" src="/javascript/menu.js"></script>
<table id="xContentPane" style="background: silver; position:absolute;" class="normal">
	<tr>
	<td>
	<asp:button cssclass="button" id="Vertical" runat="server" onclick="Vertical_OnClick"/>
	</td>
	<td><asp:button cssclass="button" id="Horizontal" runat="server" onclick="Horizontal_OnClick"/>
	</td>
	<td><asp:button cssclass="button" id="Tester" runat="server" onclick="Tester_OnClick"/>
	</td>
	<td><asp:button cssclass="button" id="Sauvegarder" runat="server" onclick="Sauvegarder_OnClick"/>
	</td>
	<td><asp:button cssclass="button" id="Retour" runat="server" onclick="Retour_OnClick"/>
	</td>
	</tr>
	<tr>
	<td colspan="5">
	<table>
	<tr>
	<td colspan="4">
	<%= DotNetZoom.getlanguage("tigra_Menu1") %>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Top") %> :</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Top" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.2")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Left") %> :</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Left" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.3")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Top") %> :</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Top" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.4")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Left") %> :</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Left" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.5")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Hauteur") %> :</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Height" runat="server" Columns="3" width="30" MaxLength="2" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.6")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Largeur") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Width" runat="server" Columns="3" width="30" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>

	<tr>
	<td colspan="4">
	<%= DotNetZoom.getlanguage("tigra_Menu2") %>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu2.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Top") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Top2" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu2.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Left") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Left2" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.3")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Top") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Top2" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.4")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Left") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Left2" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.5")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Hauteur") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Height2" runat="server" Columns="3" width="30" MaxLength="2" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.6")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Largeur") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Width2" runat="server" Columns="3" width="30" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	
	<tr>
	<td colspan="4">
	<%= DotNetZoom.getlanguage("tigra_Menu3") %>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu3.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Top") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Top3" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu3.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Block_Left") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Block_Left3" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.3")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Top") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Top3" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.4")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Left") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Left3" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.5")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Hauteur") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Height3" runat="server" Columns="3" width="30" MaxLength="2" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu1.6")) %>')" bgcolor="#ffcc00" width="75"><%= DotNetZoom.getlanguage("tigra_Largeur") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Width3" runat="server" Columns="3" width="30" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>

	<tr>
	<td colspan="4">
	Centrer le menu
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu4")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Centrer") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Centrer" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>


		
	<tr>
	<td colspan="4">
	<%= DotNetZoom.getlanguage("tigra_OpenClose") %>
	</td>
	</tr>
	<tr>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu4.1")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Ouverture") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Open_Delay" runat="server" Columns="3" width="30" MaxLength="3" CssClass="NormalTextBox"></asp:textbox>
	</td>
	<td onmouseover="this.T_WIDTH=100;return escape('<%= DotNetZoom.RTESafe(DotNetZoom.getlanguage("tigra_Menu4.2")) %>')" bgcolor="#ffcc00" width="75"  ><%= DotNetZoom.getlanguage("tigra_Fermeture") %>:</td><td bgcolor="#ffcc00" width="75">
    <asp:textbox id="Close_Delay" runat="server" Columns="3" width="30" MaxLength="4" CssClass="NormalTextBox"></asp:textbox>
	</td>
	</tr>
</table>
</td>
</tr>
</table>

<script type="text/javascript">
function dropptedMoveTo() {
 var droppedy = getCookie("droppedy");
 var droppedx = getCookie("droppedx");
var xContentPane = dd.elements.xContentPane;
if (!droppedy) {
xContentPane.moveTo('150', '200');
} else { 
xContentPane.moveTo(droppedx, droppedy);
}

}

function setCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      ((path) ? "; path=" + path : "") +
      ((domain) ? "; domain=" + domain : "") +
      ((secure) ? "; secure" : "");
  document.cookie = curCookie;
}



function getCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
}

function my_DropFunc() 
{ 

var droppedx = dd.elements.e0_0o.x; 
var droppedy = dd.elements.e0_0o.y; 
document.getElementById('tigra_Block_Top').value = droppedy;
document.getElementById('tigra_Block_Left').value = droppedx;
droppedx = dd.elements.xContentPane.x; 
droppedy = dd.elements.xContentPane.y; 
setCookie("droppedy", droppedy);
setCookie("droppedx", droppedx);
} 

<%= XtigraMenuString %>
<%= TPL_1.ToString() %>
new menu(MENU_ITEMS, MENU_POS1);
SET_DHTML("xContentPane"+CURSOR_MOVE,"e0_0o"+CURSOR_MOVE );
dropptedMoveTo();
var xMenu = dd.elements.e0_0o;
xMenu.moveTo(TempX, TempY);

</script>