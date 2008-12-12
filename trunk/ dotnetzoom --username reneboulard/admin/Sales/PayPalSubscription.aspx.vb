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


Namespace DotNetZoom

    Public Class PayPalSubscription

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

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim dr As SqlDataReader
			Dim MoneyAmount As String = ""
            Dim intRoleId As Integer = -1
            If Not Request.Params("roleid") Is Nothing Then
                intRoleId = Integer.Parse(Request.Params("roleid"))
            End If

            Dim strProcessorUserId As String
			Dim TempProcessor As String = ""
            Dim objAdmin As New AdminDB()
            dr = objAdmin.GetSinglePortal(_portalSettings.PortalId)
            If dr.Read Then
                strProcessorUserId = dr("ProcessorUserId").ToString
				TempProcessor = dr("PaymentProcessor").ToString()
            End If
            dr.Close()

            If intRoleId <> -1 And strProcessorUserId <> "" Then
				
                Dim strPayPalURL As String 
				If TempProcessor = "SandBoxPayPal" then
				strPayPalURL =  "https://www.sandbox.paypal.com/cgi-bin/webscr?"
				else
				strPayPalURL =  "https://www.paypal.com/cgi-bin/webscr?"
				end if
                If Not Request.Params("cancel") Is Nothing Then
                    ' build the cancellation PayPal URL
                    strPayPalURL += "cmd=_subscr-find&alias=" & HTTPPOSTEncode(strProcessorUserId)
                Else
                    ' build the subscription PayPal URL
                    strPayPalURL += "cmd=_ext-enter&redirect_cmd=_xclick-subscriptions&business=" & HTTPPOSTEncode(strProcessorUserId)

                    Dim objUser As New UsersDB()

                    dr = objUser.GetSingleRole(intRoleId, GetLanguage("N"))
                    If dr.Read Then
                        Dim intTrialPeriod As Integer = 1
                        If dr("TrialPeriod") <> 0 Then
                            intTrialPeriod = dr("TrialPeriod")
                        End If
                        Dim intBillingPeriod As Integer = 1
                        If dr("BillingPeriod") <> 0 Then
                            intBillingPeriod = dr("BillingPeriod")
                        End If
                    	strPayPalURL += "&custom=" & HTTPPOSTEncode(Context.User.Identity.Name)
                        strPayPalURL += "&item_name=" & HTTPPOSTEncode(_portalSettings.PortalName & " - " & dr("RoleName").ToString & " ( " & Format(dr("ServiceFee"), "0.00") & " " & _portalSettings.Currency & " " & Getlanguage("Sub_Every") & " " & intBillingPeriod.ToString & " " & dr("BillingFrequencyName") & " )")
                        strPayPalURL += "&item_number=" & HTTPPOSTEncode(intRoleId.ToString)
                        strPayPalURL += "&no_shipping=1&no_note=1"
                        If dr("TrialFrequency") <> "N" Then
                            MoneyAmount = Format(dr("TrialFee"), "#,##0.00")
                            MoneyAmount = Replace(MoneyAmount, ",", ".")
                            strPayPalURL += "&a1=" & HTTPPOSTEncode(MoneyAmount)
                            strPayPalURL += "&p1=" & HTTPPOSTEncode(intTrialPeriod)
                            strPayPalURL += "&t1=" & HTTPPOSTEncode(dr("TrialFrequency"))
                        End If
                        MoneyAmount = Format(dr("ServiceFee"), "#,##0.00")
                        MoneyAmount = Replace(MoneyAmount, ",", ".")
                        strPayPalURL += "&a3=" & HTTPPOSTEncode(MoneyAmount)
                        strPayPalURL += "&p3=" & HTTPPOSTEncode(intBillingPeriod)
                        strPayPalURL += "&t3=" & HTTPPOSTEncode(dr("BillingFrequency"))
                        strPayPalURL += "&currency_code=" & HTTPPOSTEncode(_portalSettings.Currency)
                        strPayPalURL += "&return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString())
                        strPayPalURL += "&cancel_return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString())
                        strPayPalURL += "&notify_url=http://" & HTTPPOSTEncode( GetDomainName(Request) & "/admin/Sales/PayPalIPN.aspx")

                        strPayPalURL += "&sra=1" ' reattempt on failure

                        If dr("BillingFrequency") <> "O" Then ' one-time fee
                           strPayPalURL += "&src=1"
						else
						If TempProcessor = "SandBoxPayPal" then
						strPayPalURL = "https://www.sandbox.paypal.com/xclick/business=" & HTTPPOSTEncode(strProcessorUserId)
						else
						strPayPalURL = "https://www.paypal.com/xclick/business=" & HTTPPOSTEncode(strProcessorUserId)
						end if

						
                    	strPayPalURL = strPayPalURL & "&item_name=" & HTTPPOSTEncode(_portalSettings.PortalName & " - " & dr("RoleName").ToString & " ( " & Format(dr("ServiceFee"), "0.00") & " " & _portalSettings.Currency & " "  & dr("BillingFrequencyName") & " )")
                    	strPayPalURL = strPayPalURL & "&item_number=" & HTTPPOSTEncode(intRoleId.ToString)
                    	strPayPalURL = strPayPalURL & "&quantity=1"
				        strPayPalURL = strPayPalURL & "&no_shipping=1&no_note=1"
                    	strPayPalURL = strPayPalURL & "&custom=" & HTTPPOSTEncode(Context.User.Identity.Name)
                            MoneyAmount = Format(dr("ServiceFee"), "#,##0.00")
                            MoneyAmount = Replace(MoneyAmount, ",", ".")
                            strPayPalURL = strPayPalURL & "&amount=" & HTTPPOSTEncode(MoneyAmount)
                        strPayPalURL += "&currency_code=" & HTTPPOSTEncode(_portalSettings.Currency)
                        strPayPalURL += "&return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString())
                        strPayPalURL += "&cancel_return=" & HTTPPOSTEncode(Request.UrlReferrer.ToString())
                        strPayPalURL += "&notify_url=http://" & HTTPPOSTEncode(GetDomainName(Request) & "/admin/Sales/PayPalIPN.aspx")

						strPayPalURL += "&sra=1" ' reattempt on failure
                        End If
						
                    End If
                    dr.Close()

                    If IsNumeric(Request.Params("vendorid")) Then
                        Dim objVendor As New VendorsDB()
                        dr = objVendor.GetSingleVendor(Int32.Parse(Request.Params("vendorid")))
                        If dr.Read Then
                            If dr("Country").ToString = "United States" Then
                                strPayPalURL += "&first_name=" & HTTPPOSTEncode(dr("FirstName").ToString)
                                strPayPalURL += "&last_name=" & HTTPPOSTEncode(dr("LastName").ToString)
                                strPayPalURL += "&address1=" & HTTPPOSTEncode(IIf(dr("Unit").ToString <> "", dr("Unit").ToString & " ", "") & dr("Street").ToString)
                                strPayPalURL += "&city=" & HTTPPOSTEncode(dr("City").ToString)
                                strPayPalURL += "&state=" & HTTPPOSTEncode(objAdmin.GetSingleRegion(GetLanguage("N"), , dr("Region").ToString))
                                strPayPalURL += "&zip=" & HTTPPOSTEncode(dr("PostalCode").ToString)
                            End If
                        End If
                        dr.Close()
                    Else
                        If Request.IsAuthenticated Then
                            dr = objUser.GetSingleUser(_portalSettings.PortalId, Integer.Parse(Context.User.Identity.Name))
                            If dr.Read Then
                                If dr("Country").ToString = "United States" Then
                                    strPayPalURL += "&first_name=" & HTTPPOSTEncode(dr("FirstName").ToString)
                                    strPayPalURL += "&last_name=" & HTTPPOSTEncode(dr("LastName").ToString)
                                    strPayPalURL += "&address1=" & HTTPPOSTEncode(IIf(dr("Unit").ToString <> "", dr("Unit").ToString & " ", "") & dr("Street").ToString)
                                    strPayPalURL += "&city=" & HTTPPOSTEncode(dr("City").ToString)
                                    strPayPalURL += "&state=" & HTTPPOSTEncode(objAdmin.GetSingleRegion(GetLanguage("N"), , dr("Region").ToString))
                                    strPayPalURL += "&zip=" & HTTPPOSTEncode(dr("PostalCode").ToString)
                                End If
                            End If
                            dr.Close()
                        End If
                    End If
                     
                End If

                ' redirect to PayPal
                strPayPalURL += "&lc=" + HTTPPOSTEncode(GetLanguage("N")) + "&charset=utf-8"
                Response.Redirect(strPayPalURL, True)
            Else
                Response.Redirect(Request.UrlReferrer.ToString(), True)
            End If

        End Sub

    End Class

End Namespace