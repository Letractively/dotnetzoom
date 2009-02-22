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

Namespace DotNetZoom

    Public MustInherit Class Events
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lstEvents As System.Web.UI.WebControls.DataList
        Protected WithEvents calEvents As System.Web.UI.WebControls.Calendar
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Dim arrEvents(31) As String
        Dim intMonth As Integer

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

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of event information from the Events
        ' table, and then databind the results to a templated DataList
        ' server control.  It uses the DotNetZoom.EventDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			


            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            Dim EventView As String = CType(Settings("eventview"), String)
            If EventView Is Nothing Then
                EventView = "C" ' calendar
            End If

            Dim events As New EventsDB()

            Select Case EventView
                Case "L" ' list
                    lstEvents.Visible = True
                    calEvents.Visible = False
                    ' Check to see if available in Cache
                    Dim TempKey As String = GetDBname() & "ModuleID_" & CStr(ModuleId)
                    Dim context As HttpContext = HttpContext.Current
                    Dim content As System.Data.DataTable = context.Cache(TempKey)
                    If content Is Nothing Then
                        '	Item not in cache, get it manually    
                        Dim objAdmin As New AdminDB()
                        content = AdminDB.ConvertDataReaderToDataTable(events.GetModuleEvents(ModuleId))
                        context.Cache.Insert(TempKey, content, CDp(_portalSettings.PortalId, _portalSettings.ActiveTab.TabId, ModuleId), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.Normal, Nothing)
                    End If
                    lstEvents.DataSource = content
                    lstEvents.DataBind()
                Case "C" ' calendar
                    lstEvents.Visible = False
                    calEvents.Visible = True

                    If CType(Settings("eventcalendarcellwidth"), String) <> "" Then
                        calEvents.Width = System.Web.UI.WebControls.Unit.Parse(CType(Settings("eventcalendarcellwidth"), String) & "px")
                    End If
                    If CType(Settings("eventcalendarcellheight"), String) <> "" Then
                        calEvents.Height = System.Web.UI.WebControls.Unit.Parse(CType(Settings("eventcalendarcellheight"), String) & "px")
                    End If


                    If Not Page.IsPostBack Then
                        If Not Request.Params("VisibleDate") Is Nothing Then
                            calEvents.VisibleDate = Request.Params("VisibleDate")
                        Else
                            calEvents.VisibleDate = Now
                        End If

                        Dim StartDate As Date = GetFirstDayofMonthDate(calEvents.VisibleDate)
                        Dim EndDate As Date = GetLastDayofMonthDate(calEvents.VisibleDate)

                        GetCalendarEvents(StartDate, EndDate)
                    Else
                        If calEvents.VisibleDate = #12:00:00 AM# Then
                            calEvents.VisibleDate = Now
                        End If
                    End If

            End Select

        End Sub

        Private Sub calEvents_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calEvents.DayRender

            If e.Day.Date.Month = intMonth Then
                Dim ctlLabel As Label = New Label()
                ctlLabel.Text = arrEvents(e.Day.Date.Day)
                If ctlLabel.Text <> "" Then
                    e.Cell.BackColor = System.Drawing.Color.Yellow
                    e.Cell.ForeColor = System.Drawing.Color.Red
                End If

                e.Cell.Controls.Add(ctlLabel)
            End If

        End Sub

        Private Sub calEvents_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calEvents.VisibleMonthChanged

            Dim StartDate As Date = GetFirstDayofMonthDate(e.NewDate.Date)
            Dim EndDate As Date = GetLastDayofMonthDate(e.NewDate.Date)

            GetCalendarEvents(StartDate, EndDate)

        End Sub

        Private Sub GetCalendarEvents(ByVal StartDate As Date, ByVal EndDate As Date)
            Dim events As New EventsDB()
            Dim strDayText As String
            Dim StringTip As String
            Dim datTargetDate As Date
            Dim datDate As Date
            Dim blnDisplay As Boolean
            Dim StrStartDate As String = formatansidate(StartDate.ToString("yyyy-MM-dd"))
            Dim StrEndDate As String = formatansidate(EndDate.ToString("yyyy-MM-dd"))
            Array.Clear(arrEvents, 0, 32)


            Dim dr As SqlDataReader = events.GetModuleEvents(ModuleId, CheckDateSqL(StrStartDate), CheckDateSqL(StrEndDate) & " 23:59")

            While dr.Read()
                If dr("Period").ToString = "" Then

                    strDayText = "<br>"
                    StringTip = "<span class='ItemTitle'>" & dr("Title") & "</span>"
                    If Format(dr("DateTime"), "HH:mm") <> "00:00" Then
                        StringTip += "<br>" & Format(dr("DateTime"), "HH:mm")
                    End If
                    StringTip += "<br>" & dr("Description")


                    If IsEditable Then
                        strDayText += "<a href=""" & GetFullDocument() & "?edit=control&tabid=" & TabId & "&mid=" & ModuleId & "&ItemID=" & dr("ItemID") & "&VisibleDate=" & calEvents.VisibleDate.ToShortDateString & """  title=""Modifier"">"
                    End If

                    If FormatImage(dr("IconFile")) <> "" Then
                        strDayText += "<img title="""" onmouseover=""" & ReturnToolTip(StringTip) & """ alt=""" & dr("AltText") & """ src=""" & FormatImage(dr("IconFile")) & """ border=""0"">"
                    Else
                        strDayText += "<img title="""" onmouseover=""" & ReturnToolTip(StringTip) & """ alt=""" & dr("AltText") & """ src=""" & glbPath & "images/view.gif"" width=""16"" height=""16"" border=""0"">"
                    End If
                    If IsEditable Then
                        strDayText += "</a>"
                    End If

                    arrEvents(CDate(dr("DateTime")).Day) += strDayText
                Else ' recurring event
                    datTargetDate = CType(dr("DateTime"), Date).ToLongDateString
                    datDate = Date.Parse(StartDate)
                    While datDate <= EndDate
                        blnDisplay = False
                        Select Case dr("Period").ToString
                            Case "D" ' day
                                If DateDiff(DateInterval.Day, datTargetDate, datDate) Mod dr("Every") = 0 Then
                                    blnDisplay = True
                                End If
                            Case "W" ' week
                                If DateAdd(DateInterval.WeekOfYear, DateDiff(DateInterval.WeekOfYear, datTargetDate, datDate), datTargetDate) = datDate Then
                                    If DateDiff(DateInterval.WeekOfYear, datTargetDate, datDate) Mod dr("Every") = 0 Then
                                        blnDisplay = True
                                    End If
                                End If
                            Case "M" ' month
                                If DateAdd(DateInterval.Month, DateDiff(DateInterval.Month, datTargetDate, datDate), datTargetDate) = datDate Then
                                    If DateDiff(DateInterval.Month, datTargetDate, datDate) Mod dr("Every") = 0 Then
                                        blnDisplay = True
                                    End If
                                End If
                            Case "Y" ' year
                                If DateAdd(DateInterval.Year, DateDiff(DateInterval.Year, datTargetDate, datDate), datTargetDate) = datDate Then
                                    If DateDiff(DateInterval.Year, datTargetDate, datDate) Mod dr("Every") = 0 Then
                                        blnDisplay = True
                                    End If
                                End If
                        End Select
                        If blnDisplay Then
                            If datDate < datTargetDate Then
                                blnDisplay = False
                            End If
                        End If
                        If blnDisplay Then
                            If Not IsDBNull(dr("ExpireDate")) Then
                                If datDate > CType(dr("ExpireDate"), Date).ToLongDateString Then
                                    blnDisplay = False
                                End If
                            End If
                        End If
                        If blnDisplay Then
                            strDayText = "<br>"
                            StringTip = dr("Title")
                            If Format(dr("DateTime"), "HH:mm") <> "00:00" Then
                                StringTip += "<br>" & Format(dr("DateTime"), "HH:mm")
                            End If
                            StringTip += "<br>" & dr("Description")


                            If IsEditable Then
                                strDayText += "<a href=""" & GetFullDocument() & "?edit=control&tabid=" & TabId & "&mid=" & ModuleId & "&ItemID=" & dr("ItemID") & "&VisibleDate=" & calEvents.VisibleDate.ToShortDateString & """  title=""Modifier"">"
                            End If
                            If FormatImage(dr("IconFile")) <> "" Then
                                strDayText += "<img title="""" onmouseover=""" & ReturnToolTip(StringTip) & """ alt=""" & dr("AltText") & """ src=""" & FormatImage(dr("IconFile")) & """ border=""0"">"
                            Else
                                strDayText += "<img title="""" onmouseover=""" & ReturnToolTip(StringTip) & """ alt=""" & dr("AltText") & """ src=""" & glbPath & "images/view.gif"" width=""16"" height=""16"" border=""0"">"
                            End If
                            If IsEditable Then
                                strDayText += "</a>"
                            End If


                            arrEvents(datDate.Day) += strDayText
                        End If
                        datDate = DateAdd(DateInterval.Day, 1, datDate)
                    End While
                End If
            End While
            dr.Close()

            intMonth = StartDate.Month

            calEvents.DataBind()
        End Sub


        Function FormatImage(ByVal IconFile As Object) As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Not IsDBNull(IconFile) Then
                FormatImage = _portalSettings.UploadDirectory & IconFile.ToString
            Else
                FormatImage = ""
            End If

        End Function

        Function GetFirstDayofMonth(ByVal datDate As Date) As String

            Dim datFirstDayofMonth As Date = DateSerial(datDate.Year, datDate.Month, 1)
            Return FormatAnsiDate(datFirstDayofMonth.ToString("yyyy-MM-dd"))

        End Function

        Function GetLastDayofMonth(ByVal datDate As Date) As String

            Dim intDaysInMonth As Integer = Date.DaysInMonth(datDate.Year, datDate.Month)
            Dim datLastDayofMonth As Date = DateSerial(datDate.Year, datDate.Month, intDaysInMonth)
            Return FormatAnsiDate(datLastDayofMonth.ToString("yyyy-MM-dd"))

        End Function

        Function GetFirstDayofMonthDate(ByVal datDate As Date) As Date

            Dim datFirstDayofMonth As Date = DateSerial(datDate.Year, datDate.Month, 1)
            Return datFirstDayofMonth

        End Function

        Function GetLastDayofMonthDate(ByVal datDate As Date) As Date

            Dim intDaysInMonth As Integer = Date.DaysInMonth(datDate.Year, datDate.Month)
            Dim datLastDayofMonth As Date = DateSerial(datDate.Year, datDate.Month, intDaysInMonth)
            DatLastDayOfMonth = DatLastDayOfMonth.addhours(23)
            DatLastDayOfMonth = DatLastDayOfMonth.addminutes(59)
            Return datLastDayofMonth

        End Function






    End Class

End Namespace