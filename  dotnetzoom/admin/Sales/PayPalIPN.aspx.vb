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
Imports System.Text.RegularExpressions



Namespace DotNetZoom

    Public Class PayPalIPN
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
			Dim TempstrPost As String = ""
			Dim strName As String
            Dim objStream As StreamWriter
            Dim blnValid As Boolean = True
            Dim strTransactionID As String = ""
            Dim strTransactionType As String
            Dim intRoleID As Integer = -1
            Dim intPortalID As String = -1
            Dim intUserID As Integer = -1
            Dim strDescription As String
            Dim strEmail As String
            Dim blnCancel As Boolean = False
			Dim PortalRenew As Boolean = False

            Dim objUser As New UsersDB()
            Dim objAdmin As New AdminDB()

			
            Dim strPost As String = "cmd=_notify-validate"
            For Each strName In Request.Form
                Dim strValue As String = Request.Form(strName)
                Select Case strName
                    Case "txn_type" ' get the transaction type
                        strTransactionType = strValue
                        Select Case strTransactionType
                            Case "subscr_signup", "subscr_payment", "web_accept"
                            Case "subscr_cancel"
							strDescription += " subscr_cancel "
                                blnCancel = True
                            Case Else
                                blnValid = False
                        End Select
                    Case "payment_status" ' verify the status
                        If strValue <> "Completed" Then
                            blnValid = False
                        End If
					Case "subscr_id" ' save to database the info
						strTransactionID = strValue
					Case "parent_txn_id" ' a reverse payment notification 
                    Case "txn_id" ' save to database the info 
                        strTransactionID = strValue
                    Case "receiver_email" ' verify the PayPalId
                    Case "mc_gross" ' verify the price
                        ' dblAmount = Double.Parse(strValue)
                    Case "item_number" ' get the RoleID
					If not Regex.IsMatch(strValue, "[^0-9]") and strValue <> "" then
                        intRoleID = Int32.Parse(strValue)
                        Dim dr As SqlDataReader = objUser.GetSingleRole(intRoleID, GetLanguage("N"))
                        If dr.Read Then
                            intPortalID = dr("PortalID")
                        End If
                        dr.Close()
					else
					TempstrPost += "intPortalID=NotValid" & vbcrlf
					end if
                    Case "item_name" ' get the product description
                        strDescription = strValue
                    Case "custom" ' get the UserID
					If not Regex.IsMatch(strValue, "[^0-9]") and strValue <> "" then
                        intUserID = Int32.Parse(strValue)
					else
					TempstrPost += "intUserID=NotValid" & vbcrlf
					end if
                    Case "email" ' get the email
                        strEmail = strValue
                End Select
                ' reconstruct post for postback validation
				TempstrPost += String.Format("{0}={1}" & vbcrlf, strName, strValue)
                strPost += String.Format("&{0}={1}", strName, HTTPPOSTEncode(strValue))
            Next

            ' postback to verify the source
            If blnValid Then
			
                Dim objRequest As HttpWebRequest

                Dim drp As SqlDataReader = objAdmin.GetSinglePortal(intPortalID)
				Dim TempProcessor As String = ""
                If drp.Read Then
                    TempProcessor = drp("PaymentProcessor").ToString()
                End If
                drp.Close()

				
				if TempProcessor = "SandBoxPayPal" or portalSettings.GetHostSettings("PaymentProcessor") = "SandBoxPayPal" then
				objRequest = WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr")
				else
				objRequest = WebRequest.Create("https://www.paypal.com/cgi-bin/webscr")
				end if
                objRequest.Method = "POST"
                objRequest.ContentLength = strPost.Length
                objRequest.ContentType = "application/x-www-form-urlencoded"

                objStream = New StreamWriter(objRequest.GetRequestStream())
                objStream.Write(strPost)
                objStream.Close()

                Dim objResponse As HttpWebResponse = objRequest.GetResponse()
                Dim sr As StreamReader
                sr = New StreamReader(objResponse.GetResponseStream())
                Dim strResponse As String = sr.ReadToEnd()
                sr.Close()
				TempstrPost += vbCrLf & "Response=" & strResponse
                Select Case strResponse
                    Case "VERIFIED"
					
                    Case Else
                        ' possible fraud
                        blnValid = False
                End Select
            End If

            If blnValid and intPortalID <> -1 Then

                Dim intAdministratorRoleId As String

                Dim dr As SqlDataReader = objAdmin.GetSinglePortal(intPortalID)
                If dr.Read Then
                    intAdministratorRoleId = dr("AdministratorRoleId")
                End If
                dr.Close()

                If intRoleID = intAdministratorRoleId Then
                    ' admin portal renewal
					PortalRenew = True
                    objAdmin.UpdatePortalExpiry(intPortalID)
                Else
                    ' user subscription
					PortalRenew = False
                    objUser.UpdateService(intUserID, intRoleID, blnCancel)
                End If

            End If

           ' log the results
		   If strTransactionID <> "" then
		   objAdmin.AddPayPalIPN(strTransactionID, intPortalID, intUserId, intRoleID, blnValid, TempstrPost, strDescription, PortalRenew)
		   end if
			
        End Sub

    End Class

End Namespace