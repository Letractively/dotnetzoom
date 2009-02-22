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

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection

Namespace DotNetZoom

    Public Class VendorsDB


        Public Function GetVendors(Optional ByVal PortalId As Integer = -1, Optional ByVal Filter As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null), Filter})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSingleVendor(ByVal VendorID As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorID})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return result

        End Function


        Public Function AddVendor(ByVal PortalID As Integer, ByVal VendorName As String, ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal Country As String, ByVal PostalCode As String, ByVal Telephone As String, ByVal Fax As String, ByVal Email As String, ByVal Website As String, ByVal FirstName As String, ByVal LastName As String, ByVal UserName As String, ByVal LogoFile As String, <SqlParameter(, SqlDbType.Text, , , , )> ByVal KeyWords As String, ByVal Authorized As String, <SqlParameter(, , , , , ParameterDirection.Output)> ByVal VendorID As Integer) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalID <> -1, PortalID, SqlInt16.Null), VendorName, Unit, Street, City, Region, Country, PostalCode, Telephone, Fax, Email, Website, FirstName, LastName, UserName, LogoFile, KeyWords, Boolean.Parse(Authorized), VendorID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            Return CInt(myCommand.Parameters("@VendorID").Value)

        End Function


        Public Sub UpdateVendor(ByVal VendorID As Integer, ByVal VendorName As String, ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal Country As String, ByVal PostalCode As String, ByVal Telephone As String, ByVal Fax As String, ByVal Email As String, ByVal Website As String, ByVal FirstName As String, ByVal LastName As String, ByVal UserName As String, ByVal LogoFile As String, <SqlParameter(, SqlDbType.Text, , , , )> ByVal KeyWords As String, ByVal Authorized As String)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorID, VendorName, Unit, Street, City, Region, Country, PostalCode, Telephone, Fax, Email, Website, FirstName, LastName, UserName, LogoFile, KeyWords, Boolean.Parse(Authorized)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub DeleteVendor(ByVal VendorID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Sub DeleteVendors(Optional ByVal PortalID As Integer = -1)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalID <> -1, PortalID, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function FindVendors(ByVal DisplayPortalId As Integer, Optional ByVal SelectPortalId As Integer = -1, Optional ByVal Search As String = "") As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DisplayPortalId, IIf(SelectPortalId <> -1, SelectPortalId, SqlInt16.Null), IIf(Search <> "", Search, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetVendorClickThrough(ByVal VendorId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetVendorAttributes(Optional ByVal PortalId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSingleVendorAttribute(ByVal VendorAttributeID As Integer, Optional ByVal PortalId As Integer = -1) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeID, IIf(PortalId <> -1, PortalId, SqlInt16.Null)})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return result

        End Function


        Public Sub AddVendorAttribute(ByVal PortalID As Integer, ByVal AttributeName As String, ByVal AttributeType As String, ByVal Selection As String, ByVal Direction As String, ByVal MaxLength As String, ByVal Rows As String, ByVal ViewOrder As String, ByVal IsOptional As String, ByVal IsPrivate As String)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(PortalID <> -1, PortalID, SqlInt16.Null), AttributeName, AttributeType, Selection, Direction, IIf(MaxLength <> -1, MaxLength, SqlInt16.Null), IIf(Rows <> -1, Rows, SqlInt16.Null), IIf(ViewOrder <> -1, ViewOrder, SqlInt16.Null), Boolean.Parse(IsOptional), Boolean.Parse(IsPrivate)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub UpdateVendorAttribute(ByVal VendorAttributeID As Integer, ByVal AttributeName As String, ByVal AttributeType As String, ByVal Selection As String, ByVal Direction As String, ByVal MaxLength As Integer, ByVal Rows As Integer, ByVal ViewOrder As Integer, ByVal IsOptional As String, ByVal IsPrivate As String)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeID, AttributeName, AttributeType, Selection, Direction, IIf(MaxLength <> -1, MaxLength, SqlInt16.Null), IIf(Rows <> -1, Rows, SqlInt16.Null), IIf(ViewOrder <> -1, ViewOrder, SqlInt16.Null), Boolean.Parse(IsOptional), Boolean.Parse(IsPrivate)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub DeleteVendorAttribute(ByVal VendorAttributeID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetVendorAttributeOptions(ByVal VendorAttributeID As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeID})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return result

        End Function


        Public Sub AddVendorAttributeOption(ByVal VendorAttributeId As Integer, ByVal OptionText As String, ByVal OptionValue As String, ByVal ViewOrder As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeId, OptionText, OptionValue, IIf(ViewOrder <> -1, ViewOrder, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub UpdateVendorAttributeOption(ByVal VendorAttributeOptionId As Integer, ByVal OptionText As String, ByVal OptionValue As String, ByVal ViewOrder As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeOptionId, OptionText, OptionValue, IIf(ViewOrder <> -1, ViewOrder, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub DeleteVendorAttributeOption(ByVal VendorAttributeOptionID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorAttributeOptionID})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub


        Public Function GetVendorAttributeValue(ByVal VendorId As Integer, ByVal VendorAttributeID As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, VendorAttributeID})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return result

        End Function


        Public Sub UpdateVendorAttributeValue(ByVal VendorId As Integer, ByVal VendorAttributeId As Integer, Optional ByVal Values As String = "")

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, VendorAttributeId, IIf(Values <> "", Values, SqlInt16.Null)})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Function FindBanners(ByVal DisplayPortalId As Integer, Optional ByVal BannerTypeId As Integer = -1, Optional ByVal SelectPortalId As Integer = -1, Optional ByVal Banners As Integer = 1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DisplayPortalId, IIf(BannerTypeId <> -1, BannerTypeId, SqlInt16.Null), IIf(SelectPortalId <> -1, SelectPortalId, SqlInt16.Null), Banners})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetBannerClickThrough(ByVal BannerId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetBanners(ByVal VendorId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSingleBanner(ByVal BannerId As Integer, ByVal VendorId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerId, VendorId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Sub DeleteBanner(ByVal BannerId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub AddBanner(ByVal BannerName As String, ByVal VendorId As Integer, ByVal ImageFile As String, ByVal URL As String, ByVal Impressions As Integer, ByVal CPM As Double, ByVal StartDate As String, ByVal EndDate As String, ByVal UserName As String, ByVal BannerTypeId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerName, VendorId, ImageFile, IIf(URL = "", SqlInt16.Null, URL), Impressions, CPM, IIf(StartDate = "", SqlInt16.Null, StartDate), IIf(EndDate = "", SqlInt16.Null, EndDate), UserName, BannerTypeId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub UpdateBanner(ByVal BannerId As Integer, ByVal BannerName As String, ByVal ImageFile As String, ByVal URL As String, ByVal Impressions As Integer, ByVal CPM As Double, ByVal StartDate As String, ByVal EndDate As String, ByVal UserName As String, ByVal BannerTypeId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerId, BannerName, ImageFile, IIf(URL = "", SqlInt16.Null, URL), Impressions, CPM, IIf(StartDate = "", SqlInt16.Null, StartDate), IIf(EndDate = "", SqlInt16.Null, EndDate), UserName, BannerTypeId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Function GetBannerTypes() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetBannerLog(ByVal BannerId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {BannerId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetVendorLog(ByVal VendorId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetVendorFeedback(ByVal VendorId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Function GetSingleVendorFeedback(ByVal VendorId As Integer, ByVal UserId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, UserId})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Sub UpdateVendorFeedback(ByVal VendorId As Integer, ByVal UserId As Integer, ByVal Comment As String, ByVal Value As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, UserId, Comment, Value})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Sub DeleteVendorFeedback(ByVal VendorId As Integer, ByVal UserId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, UserId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        Public Function GetVendorClassifications(Optional ByVal VendorId As Integer = -1) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {IIf(VendorId <> -1, VendorId, SqlInt16.Null)})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        Public Sub DeleteVendorClassifications(ByVal VendorId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

        Public Sub DeleteVendorClassification(ByVal ClassificationId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ClassificationId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub
		
		Public Sub AddVendorClassificationName(ByVal ClassificationName As String)
		           Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ClassificationName})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
		End Sub

        Public Sub AddVendorClassification(ByVal VendorId As Integer, ByVal ClassificationId As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {VendorId, ClassificationId})

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

    End Class

End Namespace