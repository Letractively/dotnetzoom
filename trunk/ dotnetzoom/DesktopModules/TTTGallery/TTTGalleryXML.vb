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
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
'========================================================================================
Option Strict On

Imports System.xml
Imports System.io

Namespace DotNetZoom

    Public Class GalleryXML

        ' Declare some vars
        Const _xmlFileName As String = "_metadata.xml"
        Const _datFileName As String = "_sorteddata.xml"
        Private xmlFileName As String
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

        Public ReadOnly Property GetxmlFileName() As String
            Get
                Return xmlFileName
            End Get
        End Property

        ' Creator function
        Public Sub New(ByVal Directory As String, Optional ByVal Getxml As Boolean = True)

            If Getxml Then
                xmlFileName = _xmlFileName
            Else
                xmlFileName = _datFileName
            End If
            ' Find out if metadata file exists; if it does, init some vars
            If File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                bmetaDataExists = True
                xml = New XPath.XPathDocument(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                xpath = xml.CreateNavigator()
            End If


        End Sub



        Public Function Map_Width() As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/width")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "700" 'Default Value
                End If

            Else
                Return "700" 'Default Value
            End If

        End Function

        Public Function Map_Height() As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/height")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "700" 'Default Value
                End If

            Else
                Return "700" 'Default Value
            End If

        End Function

        Public Function Map_FullScreen() As String
            '  true|false: should the map fill the entire page (or frame)
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/fullscreen")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function

        Public Function Map_center() As String
            ' latitude,longitude
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/center")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "[0,0]" 'Default Value
                End If

            Else
                Return "[0,0]" 'Default Value
            End If

        End Function

        Public Function Map_Zoom() As String
            '  higher number means closer view; can also be 'auto'
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/zoom")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'auto'" 'Default Value
                End If

            Else
                Return "'auto'" 'Default Value
            End If

        End Function

        Public Function Map_Opacity() As String
            '  number from 0 to 1
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/opacity")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "1" 'Default Value
                End If

            Else
                Return "1" 'Default Value
            End If

        End Function

        Public Function Map_Type() As String
            '  popular map_type choices are 'G_NORMAL_MAP', 'G_SATELLITE_MAP', 'G_HYBRID_MAP', 'G_PHYSICAL_MAP', 'MYTOPO_TILES'"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/type")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'G_PHYSICAL_MAP'" 'Default Value
                End If

            Else
                Return "'G_PHYSICAL_MAP'" 'Default Value
            End If

        End Function

        Public Function Map_ZoomClick() As String
            ' true|false: zoom in when mouse is double-clicked
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/zoomclick")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_ZoomMouse() As String
            ' true|false; or 'reverse' for down=in and up=out
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/zoommouse")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function

        Public Function Map_Centering() As String
            ' URL-based centering (e.g., ?center=name_of_marker&zoom=14)"
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/centering")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "{ 'open_info_window': true, 'partial_match': true, 'center_key': 'center', 'default_zoom': null}" 'Default Value
                End If

            Else
                Return "{ 'open_info_window': true, 'partial_match': true, 'center_key': 'center', 'default_zoom': null}" 'Default Value
            End If

        End Function

        Public Function Map_ZoomControl() As String
            'large'|'small'|'3d'
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/zoomcontrol")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'large'" 'Default Value
                End If

            Else
                Return "'large'" 'Default Value
            End If

        End Function

        Public Function Map_ScaleControl() As String
            'true false
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/scalecontrol")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_CenterCoord() As String
            ' true|false: show a "center coordinates" box and crosshair?
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/centercoord")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_Crosshair() As String
            ' true|false: hide the crosshair initially?"
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/crosshair")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function



        Public Function Map_OpacityCtrl() As String
            ' true|false
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/opacityctrl")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function


        Public Function Map_TypeCtrl() As String
            ' 'menu'|'list'|'none'|'google'"
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/typectrl")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'none'" 'Default Value
                End If

            Else
                Return "'none'" 'Default Value
            End If

        End Function

        Public Function Map_TypeFltr() As String
            ' true|false: when map loads, irrelevant maps ignored
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/typefltr")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_TypeExcl() As String
            ' comma-separated list of map types that will never show in the list 

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/typeexcl")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "['G_SATELLITE_3D_MAP']" 'Default Value
                End If

            Else
                Return "['G_SATELLITE_3D_MAP']" 'Default Value
            End If

        End Function

        Public Function Map_LegendOn() As String
            ' true|false: enable or disable the legend altogether"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/legendon")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function


        Public Function Map_LegendPos() As String
            ' [Google anchor name, relative x, relative y]

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/legendpos")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "['G_ANCHOR_TOP_LEFT', 70, 6]" 'Default Value
                End If

            Else
                Return "['G_ANCHOR_TOP_LEFT', 70, 6]" 'Default Value
            End If

        End Function

        Public Function Map_LegendDrag() As String
            ' true|false: can it be moved around the screen

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/legenddrag")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_LegendColl() As String
            ' true|false: can it be collapsed by double-clicking its top bar

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/legendcoll")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_Tools() As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tools")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "{ visible: false, distance_color: '', area_color: '', position: [] }" 'Default Value
                End If

            Else
                Return "{ visible: false, distance_color: '', area_color: '', position: [] }" 'Default Value
            End If

        End Function

        Public Function Map_TracklistOn() As String
            ' true|false: enable or disable the tracklist altogether

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/trackliston")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function

        Public Function Map_TracklistPos() As String
            ' [Google anchor name, relative x, relative y]"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistpos")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "['G_ANCHOR_TOP_RIGHT', 6, 32]" 'Default Value
                End If

            Else
                Return "['G_ANCHOR_TOP_RIGHT', 6, 32]" 'Default Value
            End If

        End Function

        Public Function Map_TracklistMheight() As String
            ' maximum height of the tracklist, in pixels"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistmheight")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "610" 'Default Value
                End If

            Else
                Return "610" 'Default Value
            End If

        End Function

        Public Function Map_TracklistMwidth() As String
            ' maximum width of the tracklist, in pixels"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistmwidth")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "180" 'Default Value
                End If

            Else
                Return "180" 'Default Value
            End If

        End Function

        Public Function Map_TracklistDesc() As String
            ' true|false: should tracks' descriptions be shown in the list"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistdesc")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_TracklistZoom() As String
            ' true|false: should each item include a small icon that will zoom to that track

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistzoom")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_TracklistTool() As String
            ' true|false: should the name of the track appear on the map when you mouse over the name in the list

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklisttool")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_TracklistDrag() As String
            ' true|false: can it be moved around the screen

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistdrag")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function

        Public Function Map_TracklistColl() As String
            ' true|false: can it be collapsed by double-clicking its top bar

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/tracklistcoll")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function


        Public Function Map_Marker() As String
            ' icon can be a URL, but be sure to also include size:[w,h] and optionally anchor:[x,y] 

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/marker")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "{ color: 'red', icon: 'googlemini' }" 'Default Value
                End If

            Else
                Return "{ color: 'red', icon: 'googlemini' }" 'Default Value
            End If

        End Function


        Public Function Map_Shadows() As String
            ' true|false: do the standard markers have "shadows" behind them? 

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/shadows")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "true" 'Default Value
                End If

            Else
                Return "true" 'Default Value
            End If

        End Function


        Public Function Map_LinkTgt() As String
            ' the name of the window or frame into which markers' URLs will load

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/linktgt")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'_top'" 'Default Value
                End If

            Else
                Return "'_top'" 'Default Value
            End If

        End Function


        Public Function Map_InfoWidth() As String
            ' in pixels, the width of the markers' pop-up info "bubbles" (can be overridden by 'window_width' in individual markers) 

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/infowidth")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "0" 'Default Value
                End If

            Else
                Return "0" 'Default Value
            End If

        End Function


        Public Function Map_thumbnailwidth() As String
            ' in pixels, the width of the markers' thumbnails (can be overridden by 'thumbnail_width' in individual markers)

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/thumbnailwidth")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "0" 'Default Value
                End If

            Else
                Return "0" 'Default Value
            End If

        End Function


        Public Function Map_PhotoSize() As String
            'in pixels, the size of the photos in info windows (can be overridden by 'photo_width' or 'photo_size' in individual markers) 

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/photosize")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "[0,0]" 'Default Value
                End If

            Else
                Return "[0,0]" 'Default Value
            End If

        End Function


        Public Function Map_LabelsHide() As String
            ' true|false: hide labels when map first loads?"

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/labelshide")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function


        Public Function Map_LabelOff() As String
            ' [x,y]: shift all markers' labels (positive numbers are right and down)


            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/labeloff")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "[0,0]" 'Default Value
                End If

            Else
                Return "[0,0]" 'Default Value
            End If

        End Function

        Public Function Map_LabelCenter() As String
            ' true|false: center labels with respect to their markers?  (label_left is also a valid option.)

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/labelcenter")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function

        Public Function Map_DD() As String

            ' put a small "driving directions" form in each marker's pop-up window? (override with dd:true or dd:false in a marker's options)
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/dd")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "false" 'Default Value
                End If

            Else
                Return "false" 'Default Value
            End If

        End Function

        Public Function Map_IconSet() As String
            'gpsmap' are the small 16x16 icons; change it to '24x24' for larger icons"
            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@name=""Name""]/iconset")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "'gpsmap'" 'Default Value
                End If

            Else
                Return "'gpsmap'" 'Default Value
            End If

        End Function

        Public Function Map_TrackList(ByVal Track As String) As String


            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@tracklist=""" & Track & """]/tracklist")


                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "" 'Default Value
                End If

            Else
                Return "" 'Default Value
            End If

        End Function

        Public Function Map_Markers(ByVal Marker As String) As String


            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/map[@marker=""" & Marker & """]/markers")
                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "" 'Default Value
                End If

            Else
                Return "" 'Default Value
            End If

        End Function


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
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the title data for a given filename is a folder
        Public Function Url(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/url")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the title data for a given filename is a folder
        Public Function Path(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/path")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the title data for a given filename is a folder
        Public Function GalleryHierarchy(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/galleryhierarchy")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the title data for a given filename is a folder
        Public Function Thumbnail(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/thumbnail")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the icon data for a given filename in a folder
        Public Function Icon(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/icon")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the icon data for a given filename in a folder
        Public Function Size(ByVal FileName As String) As Long

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/size")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return CLng(iterator.Current.Value())
                Else
                    Return 0
                End If

            Else
                Return 0
            End If

        End Function


        ' Gets the title data for a given filename is a folder
        Public Function Parent(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/parent")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function

        ' Gets the title data for a given filename is a folder
        Public Function Index(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/index")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
            End If

        End Function


        ' Gets the title data for a given filename is a folder
        Public Function IsFolder(ByVal FileName As String) As Boolean

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/isfolder")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return CBool(iterator.Current.Value())
                Else
                    Return False
                End If

            Else
                Return False
            End If

        End Function

        ' Gets the sort data for a given filename is a folder
        Public Function Sort(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As xpath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/sort")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
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
                    Return ""
                End If

            Else
                Return ""
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


        ' Gets the latitude a given filename is a folder
        Public Function latitude(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/lat")

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
        ' Gets the Longitude a given filename is a folder
        Public Function Longitude(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/long")

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
        ' Gets the gpsicon a given filename is a folder
        Public Function gpsicon(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/gpsicon")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return "/images/gps/24camera.png"
                End If

            Else
                Return "/images/gps/24camera.png"
            End If



        End Function

        ' Gets the gpsicon a given filename is a folder
        Public Function gpsiconsize(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/gpsiconsize")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    If iterator.Current.Value() <> "" Then
                        Return iterator.Current.Value()
                    Else
                        Return "[24,24]"
                    End If
                Else
                    Return "[24,24]"
                End If

            Else
                Return "[24,24]"
            End If

        End Function



        ' Gets the link 
        Public Function Link(ByVal FileName As String) As String

            If bmetaDataExists Then
                ' Only run this if we have an XML file

                Dim iterator As XPath.XPathNodeIterator

                ' Get the iterator object for given Xpath search
                iterator = xpath.Select("/files/file[@name=""" & FileName & """]/link")

                If iterator.MoveNext() Then
                    ' If we moved, then we retrieved a result
                    Return iterator.Current.Value()
                Else
                    Return ""
                End If

            Else
                Return ""
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
                    Return ""
                End If

            Else
                Return ""
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

        Public Function CompleteInfo(ByVal FileName As String) As IGalleryObjectInfo

            Dim What As New DataInfo

            What.Name = FileName
            If bmetaDataExists Then

                What.Categories = Categories(FileName)
                What.Description = Description(FileName)
                What.Height = height(FileName)
                What.Width = Width(FileName)
                What.Icon = Icon(FileName)
                ' What.Index = 
                What.IsFolder = CBool(IsFolder(FileName))
                'What.Type = 
                What.OwnerID = OwnerID(FileName)
                'What.Path = 
                What.Size = CType(Size(FileName), Long)
                What.Sort = Sort(FileName)
                ' What.Thumbnail = 
                What.Title = Title(FileName)
                'What.URL = FileNode.Item("url").InnerText
                What.Latitude = latitude(FileName)
                What.Longitude = Longitude(FileName)
                What.Gpsicon = gpsicon(FileName)
                What.Gpsiconsize = gpsiconsize(FileName)
                What.Link = Link(FileName)

            End If
            Return CType(What, IGalleryObjectInfo)
        End Function

        Public Shared Function ObjectInfo(ByVal Directory As String) As ArrayList
            Dim WhatArray As ArrayList = New ArrayList()
            Dim xmlDoc As New XmlDocument()
            Dim FileNode As XmlNode

            ' Check for existence of file 
            If Not File.Exists(BuildPath(New String(1) {Directory, _datFileName}, "\", False, True)) Then
                Return Nothing
            Else

                Dim i As Integer

                xmlDoc.Load(BuildPath(New String(1) {Directory, _datFileName}, "\", False, True))

                For Each FileNode In xmlDoc.SelectNodes("//files/file")
                    Dim What As New DataInfo
                    What.Name = FileNode.Attributes.GetNamedItem("name").InnerText
                    What.Categories = FileNode.Item("categories").InnerText
                    What.Description = FileNode.Item("description").InnerText
                    What.Height = FileNode.Item("height").InnerText
                    What.Icon = FileNode.Item("icon").InnerText
                    What.Index = CInt(FileNode.Item("index").InnerText)
                    What.IsFolder = CBool(FileNode.Item("isfolder").InnerText)

                    Select Case FileNode.Item("type").InnerText
                        Case "Folder"
                            What.Type = IGalleryObjectInfo.ItemType.Folder
                        Case "Image"
                            What.Type = IGalleryObjectInfo.ItemType.Image
                        Case "Movie"
                            What.Type = IGalleryObjectInfo.ItemType.Movie
                        Case "Flash"
                            What.Type = IGalleryObjectInfo.ItemType.Flash
                        Case "Zip"
                            What.Type = IGalleryObjectInfo.ItemType.Zip

                    End Select

                    What.OwnerID = CInt(FileNode.Item("ownerid").InnerText)
                    What.Path = FileNode.Item("path").InnerText
                    What.Size = CType(FileNode.Item("size").InnerText, Long)
                    What.Sort = FileNode.Item("sort").InnerText
                    What.ThumbNail = FileNode.Item("thumbnail").InnerText
                    What.Title = FileNode.Item("title").InnerText
                    What.URL = FileNode.Item("url").InnerText
                    What.Width = FileNode.Item("width").InnerText
                    WhatArray.Add(What)
                Next

                Return WhatArray
            End If
        End Function



        Public Shared Sub DeleteMetaData(ByVal Directory As String, ByVal Name As String, Optional ByVal Getxml As Boolean = True)
            Dim xmlFileName As String
            If Getxml Then
                xmlFileName = _xmlFileName
            Else
                xmlFileName = _datFileName
            End If

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

        Public Shared Sub SaveSortedData(ByVal Directory As String, ByVal WhatToSave As ArrayList)
            Dim xmlFileName As String = _datFileName
            ' Shared sub for saving a file's metadata to XML
            Dim root, newNode, subNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()
            Dim What As IGalleryObjectInfo
            Dim I As Integer


            ' Create the declaration node and root node and add them
            newNode = xml.CreateNode(XmlNodeType.XmlDeclaration, "xml", Nothing)
            root = xml.CreateElement("files")

            xml.AppendChild(newNode)
            xml.AppendChild(root)

            For I = 0 To WhatToSave.Count - 1

                What = CType(WhatToSave(I), IGalleryObjectInfo)


                'Regardless of the circumstance, we need to create a new node for the update
                newNode = xml.CreateElement("file")


                attr = xml.CreateAttribute("name")
                attr.Value = What.Name
                newNode.Attributes.Append(attr)


                subNode = xml.CreateElement("isfolder")
                subNode.InnerText = What.IsFolder.ToString()
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("title")
                subNode.InnerText = What.Title
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("description")
                subNode.InnerText = What.Description
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("categories")
                subNode.InnerText = What.Categories
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("ownerid")
                subNode.InnerText = What.OwnerID.ToString
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("width")
                subNode.InnerText = What.Width
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("height")
                subNode.InnerText = What.Height
                newNode.AppendChild(subNode)


                subNode = xml.CreateElement("lat")
                subNode.InnerText = What.latitude
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("long")
                subNode.InnerText = What.longitude
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("gpsicon")
                subNode.InnerText = What.gpsicon
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("gpsiconsize")
                subNode.InnerText = What.gpsiconsize
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("link")
                subNode.InnerText = What.link
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("url")
                subNode.InnerText = What.URL
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("path")
                subNode.InnerText = What.Path
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("thumbnail")
                subNode.InnerText = What.Thumbnail
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("icon")
                subNode.InnerText = What.Icon
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("index")
                subNode.InnerText = What.Index.ToString()
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("size")
                subNode.InnerText = What.Size.ToString
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("sort")
                subNode.InnerText = What.Sort.ToString
                newNode.AppendChild(subNode)

                subNode = xml.CreateElement("type")
                subNode.InnerText = What.Type.ToString
                newNode.AppendChild(subNode)

                root.AppendChild(newNode)
            Next

            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub


        Public Shared Sub Savegpsicon(ByVal Directory As String, ByVal FileName As String, ByVal Icon As String, ByVal IconSize As String)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, oldnode, newNode As XmlNode
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then

                ' Load the XML and init a few items
                xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                root = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]")

                If Not root Is Nothing Then

                    'Regardless of the circumstance, we need to create a new node for the update

                    newNode = xml.CreateElement("gpsicon")
                    newNode.InnerText = Icon
                    ' Look for the existence of this node already
                    oldnode = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]/gpsicon")

                    If oldnode Is Nothing Then
                        ' Node was not found.  Add it
                        root.AppendChild(newNode)
                    Else
                        ' Node was found.  Need to update instead
                        root.ReplaceChild(newNode, oldnode)
                    End If

                    newNode = xml.CreateElement("gpsiconsize")
                    newNode.InnerText = IconSize
                    ' Look for the existence of this node already
                    oldnode = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]/gpsiconsize")

                    If oldnode Is Nothing Then
                        ' Node was not found.  Add it
                        root.AppendChild(newNode)
                    Else
                        ' Node was found.  Need to update instead
                        root.ReplaceChild(newNode, oldnode)
                    End If

                    ' Now save the file back so that the updates are saved.
                    xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                End If
            End If
        End Sub

        Public Shared Sub SaveLatLong(ByVal Directory As String, ByVal FileName As String, ByVal Lat As String, ByVal Longitude As String)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, oldnode, newNode As XmlNode
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then

                ' Load the XML and init a few items
                xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                root = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]")

                If Not root Is Nothing Then

                    'Regardless of the circumstance, we need to create a new node for the update

                    newNode = xml.CreateElement("lat")
                    newNode.InnerText = Lat
                    ' Look for the existence of this node already
                    oldnode = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]/lat")

                    If oldnode Is Nothing Then
                        ' Node was not found.  Add it
                        root.AppendChild(newNode)
                    Else
                        ' Node was found.  Need to update instead
                        root.ReplaceChild(newNode, oldnode)
                    End If

                    newNode = xml.CreateElement("long")
                    newNode.InnerText = Longitude
                    ' Look for the existence of this node already
                    oldnode = xml.SelectSingleNode("/files/file[@name=""" & FileName & """]/long")

                    If oldnode Is Nothing Then
                        ' Node was not found.  Add it
                        root.AppendChild(newNode)
                    Else
                        ' Node was found.  Need to update instead
                        root.ReplaceChild(newNode, oldnode)
                    End If

                    ' Now save the file back so that the updates are saved.
                    xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
                End If
            End If
        End Sub

        Public Shared Sub SaveMetaData(ByVal Sort As String, ByVal Directory As String, ByVal FileName As String, ByVal Title As String, ByVal Description As String, ByVal Categories As String, ByVal OwnerID As Integer, ByVal Width As String, ByVal Height As String, ByVal latitude As String, ByVal longitude As String, ByVal gpsicon As String, ByVal gpsiconsize As String, ByVal link As String)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, fileNode, newNode, subNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()


            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
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
            subNode.InnerText = Width
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("height")
            subNode.InnerText = Height
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("sort")
            subNode.InnerText = Sort
            newNode.AppendChild(subNode)


            subNode = xml.CreateElement("lat")
            subNode.InnerText = latitude
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("long")
            subNode.InnerText = longitude
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("gpsicon")
            subNode.InnerText = gpsicon
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("gpsiconsize")
            subNode.InnerText = gpsiconsize
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("link")
            subNode.InnerText = link
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


        Public Shared Sub SaveGalleryData(ByVal Directory As String, ByVal What As IGalleryObjectInfo)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, fileNode, newNode, subNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()


            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("file")

            attr = xml.CreateAttribute("name")
            attr.Value = What.Name
            newNode.Attributes.Append(attr)

            subNode = xml.CreateElement("title")
            subNode.InnerText = What.Title
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("description")
            subNode.InnerText = What.Description
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("categories")
            subNode.InnerText = What.Categories
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("ownerid")
            subNode.InnerText = What.OwnerID.ToString
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("width")
            subNode.InnerText = What.Width
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("height")
            subNode.InnerText = What.Height
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("sort")
            subNode.InnerText = What.Sort()
            newNode.AppendChild(subNode)


            subNode = xml.CreateElement("lat")
            subNode.InnerText = What.Latitude
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("long")
            subNode.InnerText = What.Longitude
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("gpsicon")
            subNode.InnerText = What.Gpsicon
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("gpsiconsize")
            subNode.InnerText = What.Gpsiconsize
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("link")
            subNode.InnerText = What.Link
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("isfolder")
            subNode.InnerText = What.IsFolder.ToString
            newNode.AppendChild(subNode)

            subNode = xml.CreateElement("size")
            subNode.InnerText = What.Size.ToString
            newNode.AppendChild(subNode)


            ' We now have the complete node to add/update

            ' Look for the existence of this node already
            fileNode = xml.SelectSingleNode("/files/file[@name=""" & What.Name & """]")

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


        Public Shared Sub SaveMapMetaData(ByVal Directory As String, _
                                          ByVal width As String, _
                                          ByVal height As String, _
                                          ByVal fullscreen As String, _
                                          ByVal center As String, _
                                          ByVal zoom As String, _
                                          ByVal opacity As String, _
                                          ByVal type As String, _
                                          ByVal zoomclick As String, _
                                          ByVal zoommouse As String, _
                                          ByVal centering As String, _
                                          ByVal zoomcontrol As String, _
                                          ByVal scalecontrol As String, _
                                          ByVal centercoord As String, _
                                          ByVal crosshair As String, _
                                          ByVal opacityctrl As String, _
                                          ByVal typectrl As String, _
                                          ByVal typefltr As String, _
                                          ByVal typeexcl As String, _
                                          ByVal legendon As String, _
                                          ByVal legendpos As String, _
                                          ByVal legenddrag As String, _
                                          ByVal legendcoll As String, _
                                          ByVal tools As String, _
                                          ByVal trackliston As String, _
                                          ByVal tracklistpos As String, _
                                          ByVal tracklistmwidth As String, _
                                          ByVal tracklistmheight As String, _
                                          ByVal tracklistdesc As String, _
                                          ByVal tracklistzoom As String, _
                                          ByVal tracklisttool As String, _
                                          ByVal tracklistdrag As String, _
                                          ByVal tracklistcoll As String, _
                                          ByVal marker As String, _
                                          ByVal xshadows As String, _
                                          ByVal linktgt As String, _
                                          ByVal infowidth As String, _
                                          ByVal thumbnailwidth As String, _
                                          ByVal photosize As String, _
                                          ByVal labelshide As String, _
                                          ByVal labeloff As String, _
                                          ByVal labelcenter As String, _
                                          ByVal dd As String, _
                                          ByVal iconset As String)


            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, newNode, subnode, mapNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("map")

            attr = xml.CreateAttribute("name")
            attr.Value = "Name"
            newNode.Attributes.Append(attr)

            subnode = xml.CreateElement("width")
            subnode.InnerText = width
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("height")
            subnode.InnerText = height
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("fullscreen")
            subnode.InnerText = fullscreen
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("center")
            subnode.InnerText = center
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("zoom")
            subnode.InnerText = zoom
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("opacity")
            subnode.InnerText = opacity
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("type")
            subnode.InnerText = type
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("zoomclick")
            subnode.InnerText = zoomclick
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("zoommouse")
            subnode.InnerText = zoommouse
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("centering")
            subnode.InnerText = centering
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("zoomcontrol")
            subnode.InnerText = zoomcontrol
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("scalecontrol")
            subnode.InnerText = scalecontrol
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("centercoord")
            subnode.InnerText = centercoord
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("crosshair")
            subnode.InnerText = crosshair
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("opacityctrl")
            subnode.InnerText = opacityctrl
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("typectrl")
            subnode.InnerText = typectrl
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("typefltr")
            subnode.InnerText = typefltr
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("typeexcl")
            subnode.InnerText = typeexcl
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("legendon")
            subnode.InnerText = legendon
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("legendpos")
            subnode.InnerText = legendpos
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("legenddrag")
            subnode.InnerText = legenddrag
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("legendcoll")
            subnode.InnerText = legendcoll
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tools")
            subnode.InnerText = tools
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("trackliston")
            subnode.InnerText = trackliston
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistpos")
            subnode.InnerText = tracklistpos
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistmwidth")
            subnode.InnerText = tracklistmwidth
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistmheight")
            subnode.InnerText = tracklistmheight
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistdesc")
            subnode.InnerText = tracklistdesc
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistzoom")
            subnode.InnerText = tracklistzoom
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklisttool")
            subnode.InnerText = tracklisttool
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistdrag")
            subnode.InnerText = tracklistdrag
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("tracklistcoll")
            subnode.InnerText = tracklistcoll
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("marker")
            subnode.InnerText = marker
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("shadows")
            subnode.InnerText = xshadows
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("linktgt")
            subnode.InnerText = linktgt
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("infowidth")
            subnode.InnerText = infowidth
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("thumbnailwidth")
            subnode.InnerText = thumbnailwidth
            newNode.AppendChild(subnode)

            subnode = xml.CreateElement("photosize")
            subnode.InnerText = photosize
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("labelshide")
            subnode.InnerText = labelshide
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("labeloff")
            subnode.InnerText = labeloff
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("labelcenter")
            subnode.InnerText = labelcenter
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("dd")
            subnode.InnerText = dd
            newNode.AppendChild(subnode)


            subnode = xml.CreateElement("iconset")
            subnode.InnerText = iconset
            newNode.AppendChild(subnode)



            ' We now have the complete node to add/update

            ' Look for the existence of this node already
            mapNode = xml.SelectSingleNode("/files/map[@name=""Name""]")

            If mapNode Is Nothing Then
                ' Node was not found.  Add it
                root.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                root.ReplaceChild(newNode, mapNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        Public Shared Sub SaveMapTrackList(ByVal Directory As String, ByVal Track As String, ByVal TrackList As String)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, newNode, subnode, mapNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("map")

            attr = xml.CreateAttribute("tracklist")
            attr.Value = Track
            newNode.Attributes.Append(attr)

            subnode = xml.CreateElement("tracklist")
            subnode.InnerText = TrackList
            newNode.AppendChild(subnode)


            ' We now have the complete node to add/update

            ' Look for the existence of this node already

            mapNode = xml.SelectSingleNode("/files/map[@tracklist=""" & Track & """]")

            If mapNode Is Nothing Then
                ' Node was not found.  Add it
                root.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                root.ReplaceChild(newNode, mapNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub

        Public Shared Sub SaveMapMarkers(ByVal Directory As String, ByVal Marker As String, ByVal Markers As String)
            Dim xmlFileName As String = _xmlFileName
            ' Shared sub for saving a file's metadata to XML

            Dim root, newNode, subnode, mapNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
            End If

            ' Load the XML and init a few items
            xml.Load(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))
            root = xml.DocumentElement

            'Regardless of the circumstance, we need to create a new node for the update
            newNode = xml.CreateElement("map")

            attr = xml.CreateAttribute("marker")
            attr.Value = Marker
            newNode.Attributes.Append(attr)

            subnode = xml.CreateElement("markers")
            subnode.InnerText = Markers
            newNode.AppendChild(subnode)


            ' We now have the complete node to add/update

            ' Look for the existence of this node already

            mapNode = xml.SelectSingleNode("/files/map[@marker=""" & Marker & """]")

            If mapNode Is Nothing Then
                ' Node was not found.  Add it
                root.AppendChild(newNode)
            Else
                ' Node was found.  Need to update instead
                root.ReplaceChild(newNode, mapNode)
            End If

            ' Now save the file back so that the updates are saved.
            xml.Save(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True))

        End Sub



        Private Shared Sub AddCatetory(ByVal Directory As String, ByVal Value As String, Optional ByVal Getxml As Boolean = True)
            Dim xmlFileName As String
            If Getxml Then
                xmlFileName = _xmlFileName
            Else
                xmlFileName = _datFileName
            End If

            Dim root, catNode, newNode, oldNode As XmlNode
            Dim attr As XmlAttribute
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
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
            Dim iterator As XPath.XPathNodeIterator
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

        Private Shared Sub AddGalleryOwner(ByVal Directory As String, ByVal Value As String, Optional ByVal Getxml As Boolean = True)
            Dim xmlFileName As String
            If Getxml Then
                xmlFileName = _xmlFileName
            Else
                xmlFileName = _datFileName
            End If

            Dim root, newNode, oldNode As XmlNode
            Dim xml As New XmlDocument()

            ' Check for existence of file and build if necessary
            If Not File.Exists(BuildPath(New String(1) {Directory, xmlFileName}, "\", False, True)) Then
                CreateMetaDataFile(Directory, xmlFileName)
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

            Dim iterator As XPath.XPathNodeIterator

            ' Get the iterator object for given Xpath search
            iterator = xpath.Select("files/owner")

            If iterator.MoveNext Then
                ' If we moved, then we retrieved a result
                Return Int16.Parse(iterator.Current.Value())
            Else
                Return 0
            End If

        End Function

        Private Shared Sub CreateMetaDataFile(ByVal Directory As String, ByVal xmlFileName As String)

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