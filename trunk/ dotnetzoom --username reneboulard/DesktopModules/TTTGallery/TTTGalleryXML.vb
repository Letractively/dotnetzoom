'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
'========================================================================================
Option Strict On

Imports System.xml
Imports System.io

Namespace DotNetZoom

    Public Class GalleryXML

        ' Declare some vars
        Const xmlFileName As String = "_metadata.xml"
        Private xml As xpath.XPathDocument
        Private xpath As xpath.XPathNavigator

        Private bmetaDataExists As Boolean = False

        ' Determines whether the metadata files exists; allows us to speed up processing
        ' if it doesn't
        Public ReadOnly Property MetaDataExists() As Boolean
            Get
                Return bmetaDataExists
            End Get
        End Property

        ' Creator function
        Public Sub New(ByVal Directory As String)

            ' Find out if metadata file exists; if it does, init some vars
            If File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                bmetaDataExists = True
                xml = New xpath.XPathDocument(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                xpath = xml.CreateNavigator()
            End If

        End Sub

		
	
        ' Gets the title data for a given filename is a folder
        Public Function Title(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/title")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return String.Empty
                End If

            Else
                Return String.Empty
            End If

        End Function

        ' Gets the description data for a given filename is a folder
        Public Function Description(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/description")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return String.Empty
                End If

            Else
                Return String.Empty
            End If

        End Function


        ' Gets the Width a given filename is a folder
        Public Function Width(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/width")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "0"
                End If

            Else
                Return "0"
            End If

        End Function


        ' Gets the Height a given filename is a folder
        Public Function height(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/height")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "0"
                End If

            Else
                Return "0"
            End If

        End Function

		


        ' Gets the category data for a given filename is a folder
        Public Function Categories(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/categories")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return String.Empty
                End If

            Else
                Return String.Empty
            End If

        End Function

        ' Gets the category data for a given filename is a folder
        Public Function OwnerID(ByVal FileName As String) As Integer

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/ownerid")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return Int16.Parse(iterator.Current.Value())
                Else
                    Return 0
                End If

            Else
                Return 0
            End If

        End Function

		
        Public Shared Sub DeleteMetaData(ByVal Directory As String, ByVal Name As String)
            Dim xml As New XmlDocument()
            Dim root, fileNode As XmlNode

            ' Check for existence of file 
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                Return
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement
            fileNode = xml.SelectSingleNode("/files/file[@name=""" & Name & """]")

            If Not fileNode Is Nothing Then
                root.RemoveChild(fileNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        Public Shared Sub SaveMetaData(ByVal Directory As String, ByVal FileName As String, ByVal Title As String, ByVal Description As String, ByVal Categories As String, ByVal OwnerID As Integer, ByVal Width As String, ByVal Height As String)
            ' Shared sub for saving a file's metadata to XML

            Dim root, fileNode, newNode, subNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("file")

            attr = xml.CreateAttribute("name")
            attr.Value = FileName
            newNode.Attributes.Append(attr)

            subNode = xml.CreateElement("title")
            subNode.InnerText = Title
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("description")
            subNode.InnerText = Description
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("categories")
            subNode.InnerText = Categories
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("ownerid")
            subNode.InnerText = OwnerID.ToString
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("width")
            subNode.InnerText = width
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("height")
            subNode.InnerText = height
            newNode.AppendChild(subNode)
			
			
            ' We now have the complete node to add/update

            ' Look for the existence of this node already
            fileNode = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]")

            If fileNode Is Nothing Then
                ' Node was not found.  Add it
                root.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                root.ReplaceChild(newNode, fileNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        Private Shared Sub AddCatetory(ByVal Directory As String, ByVal Value As String)

            Dim root, catNode, newNode, oldNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement
            catNode = root.SelectSingleNode("categories")

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("category")

            attr = xml.CreateAttribute("value")
            attr.Value = Value
            newNode.Attributes.Append(attr)

            ' We now have the complete node to add/update

            ' Look for the existence of this node already
            oldNode = root.SelectSingleNode("/categories/category[@value='" & Value & "']")

            If oldNode Is Nothing Then
                ' Node was not found.  Add it
                catNode.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                catNode.ReplaceChild(newNode, oldNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        ' Gets the category data for a given filename is a folder
        Public Function GetCategories() As ArrayList

            If Not bmetaDataExists Then Return Nothing
            Dim catList As New ArrayList()
            Dim iterator As xpath.XPathNodeIterator
            Dim i As Integer

            ' Get the iterator object for given Xpath search
            iterator = xpath.Select("files/categories/category")

            For i = 0 To iterator.Count - 1
                If iterator.MoveNext Then
                    ' If we moved, then we retrieved a result
                    catList.Add(iterator.Current.GetAttribute("value", ""))
                End If
            Next
            Return catList

        End Function

        Private Shared Sub AddGalleryOwner(ByVal Directory As String, ByVal Value As String)

            Dim root, newNode, oldNode As XmlNode
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("owner")
            newNode.InnerText = Value

            ' We now have the complete node to add/update

            ' Look for the existence of this node already
            oldNode = root.SelectSingleNode("owner")

            If oldNode Is Nothing Then
                ' Node was not found.  Add it
                root.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                root.ReplaceChild(newNode, oldNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        ' Gets the category data for a given filename is a folder
        Public Function GetGalleryOwner() As Integer

            If Not bmetaDataExists Then Return 0

            Dim iterator As xpath.XPathNodeIterator

            ' Get the iterator object for given Xpath search
            iterator = xpath.Select("files/owner")

            If iterator.MoveNext Then
                ' If we moved, then we retrieved a result
                Return Int16.Parse(iterator.Current.Value())
            Else
                Return 0
            End If

        End Function

        Private Shared Sub CreateMetaDataFile(ByVal Directory As String)
            ' Creates new XML metadata file for a particular folder

            Dim xml As New XmlDocument()
            Dim rootNode, newNode As XmlNode

            ' Create the declaration node and root node and add them
            newNode = xml.CreateNode(XmlNodeType.XmlDeclaration, "xml", Nothing)
            rootNode = xml.CreateElement("files")

            xml.AppendChild(newNode)
            xml.AppendChild(rootNode)

            ' Now, save the file
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub



    End Class

End Namespace