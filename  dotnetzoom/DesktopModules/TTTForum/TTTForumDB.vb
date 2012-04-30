'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.9)
'======================================================================================= 
' For TTTCompany                    http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' With ideas/code contributed by:   JOE BRINKMAN    (Jbrinkman - joe.brinkman@tag-software.net) 
'                                   SAM HUNT        (Ossy - Sam.Hunt@nastech.eds.com)
'                                   CLEM MESSERLI   (Webguy96 - webguy96@hotmail.com)
'=======================================================================================
Option Strict On

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Reflection
Imports DotNetZoom.TTTUtils
Imports System.Web.HttpUtility
Imports System.IO

Namespace DotNetZoom

#Region "ForumDB"

    Public Class ForumDB       

#Region "ForumGroup"

        Public Function TTTForum_GetGroups(ByVal PortalID As Integer, ByVal ModuleID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, ModuleID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_GetSingleGroup(ByVal ForumGroupID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_ForumGroupCreateUpdateDelete( _
            ByVal ForumGroupID As Integer, _
            ByVal Name As String, _
            ByVal PortalID As Integer, _
            ByVal ModuleID As Integer, _
            ByVal CreatedByUser As Integer, _
            ByVal Action As Integer, _
            <SqlParameter(, , , , , ParameterDirection.Output)> Optional ByVal CreatedForumGroupID As Integer = -1) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Adding: ForumGroupID = 0,              Action = 0
            'Update: ForumGroupID = ForumGroupID,   Action = 1
            'Delete: ForumGroupID = ForumGroupID,   Action = 2

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID, Name, PortalID, ModuleID, CreatedByUser, Action, CreatedForumGroupID})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
                Throw Exc
                ' duplicate group
            End Try


            ' IIf(IsDBNull(dr("Country_Code")), "", dr("Country_Code"))
            Return CInt(IIf(IsDBNull(myCommand.Parameters("@CreatedForumGroupID").Value), ForumGroupID, myCommand.Parameters("@CreatedForumGroupID").Value))

        End Function

        Public Sub TTTForum_UpdateForumGroupSortOrder(ByVal ForumGroupID As Integer, ByVal MoveUp As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID, MoveUp})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

        End Sub

#End Region

#Region "ForumItem"

        Public Function TTTForum_GetForums(ByVal ForumGroupID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_GetSingleForum(ByVal ForumID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_ForumCreateUpdateDelete( _
            ByVal ForumGroupID As Integer, _
            ByVal PortalID As Integer, _
            ByVal ModuleID As Integer, _
            ByVal Name As String, _
            ByVal Description As String, _
            ByVal CreatedByUser As Integer, _
            ByVal IsModerated As Boolean, _
            ByVal IsActive As Boolean, _
            ByVal IsIntegrated As Boolean, _
            ByVal IntegratedGallery As Integer, _
            ByVal IntegratedAlbumName As String, _
            ByVal IsPrivate As Boolean, _
            ByVal AuthorizedRoles As String, _
            ByVal Action As Integer, _
            ByVal ForumID As Integer, _
            <SqlParameter(, , , , , ParameterDirection.Output)> Optional ByVal OutputForumID As Integer = -1) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Adding: ForumID = 0,              Action = 0
            'Update: ForumID = ForumGroupID,   Action = 1
            'Delete: ForumID = ForumGroupID,   Action = 2

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID, PortalID, ModuleID, Name, Description, CreatedByUser, IsModerated, IsActive, IsIntegrated, IntegratedGallery, IntegratedAlbumName, IsPrivate, AuthorizedRoles, Action, ForumID, OutputForumID})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch Exc As System.Exception
                Dim strExc As String = Exc.ToString
                ' duplicate Forum
            End Try

            myConnection.Close()
            Return CInt(IIf(IsDBNull(myCommand.Parameters("@OutputForumID").Value), ForumID, myCommand.Parameters("@OutputForumID").Value))



        End Function

        Public Sub TTTForum_UpdateForumSortOrder(ByVal ForumGroupID As Integer, ByVal ForumID As Integer, ByVal MoveUp As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumGroupID, ForumID, MoveUp})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

        End Sub

        Public Sub TTTForum_TrackingForumCreateDelete(ByVal ForumID As Integer, ByVal UserID As Integer, ByVal Add As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID, UserID, Add})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
				myConnection.Close()
            Catch Exc As System.Exception
                Dim strExc As String = Exc.ToString
                ' duplicate Forum
            End Try

        End Sub

        Public Function TTTForum_TrackingForumGet(ByVal UserID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_PrivateForum_GetRoles(ByVal ForumID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Sub TTTForum_PrivateForum_CreateDeleteRoles(ByVal ForumID As Integer, ByVal RoleID As Integer, ByVal Add As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID, RoleID, Add})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
				myConnection.Close()
            Catch Exc As System.Exception
                Dim strExc As String = Exc.ToString
            End Try

        End Sub

        Public Function TTTForum_GetAvatarsModule(ByVal ForumID As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_GetModuleIDByTitle(ByVal ModuleTitle As String, ByVal ModuleDefID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ModuleTitle, ModuleDefID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

#End Region

#Region "ForumThread"

        Public Function TTTForum_GetThreads(ByVal ForumID As Integer, ByVal PageSize As Integer, ByVal pageIndex As Integer, ByVal Filter As String) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID, PageSize, pageIndex, Filter})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_GetSingleThread(ByVal ThreadID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ThreadID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Shared Function TTTForum_GetThreadRepliesCount(ByVal threadID As Integer) As Integer
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_GetRepliesFromThread", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@Replies", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = threadID
            cmd.Parameters(1).Direction = ParameterDirection.Output

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Return ConvertInteger(cmd.Parameters(1).Value)

        End Function 'TTTForum_GetThreadRepliesCount

        Public Shared Function TTTForum_GetThreadCount(ByVal forumID As Integer) As Integer
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_GetThreadCount", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@Count", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = forumID
            cmd.Parameters(1).Direction = ParameterDirection.Output

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Return ConvertInteger(cmd.Parameters(1).Value)

        End Function 'TTTForum_GetThreadCount

        Public Shared Function TTTForum_GetThreadFromPost(ByVal postID As Integer) As Integer
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_GetThreadFromPost", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@PostID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = postID
            cmd.Parameters(1).Direction = ParameterDirection.Output

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Return ConvertInteger(cmd.Parameters(1).Value)

        End Function 'TTTForum_GetThreadFromPost

        Public Shared Sub TTTForum_IncrementThreadViews(ByVal threadID As Integer)
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_IncrementThreadViews", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = threadID

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch Exc As System.Exception
            End Try

        End Sub 'TTTForum_IncrementThreadViews

        Public Function TTTForum_TrackingThreadExists(ByVal ThreadID As Integer, ByVal UserID As Integer) As Boolean

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ThreadID, UserID})

            ' Execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader
            Dim result As Boolean = False

            If dr.Read Then
                result = ConvertBoolean(dr("IsUserTrackingThread"))
            End If

            dr.Close()
            Return result

        End Function

        Public Sub TTTForum_TrackingThreadCreateDelete(ByVal ThreadID As Integer, ByVal UserID As Integer, ByVal Add As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ThreadID, UserID, Add})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
				myConnection.Close()
            Catch Exc As System.Exception
                Dim strExc As String = Exc.ToString
                ' duplicate Forum
            End Try

        End Sub

        Public Function TTTForum_TrackingThreadEmails(ByVal PostID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

#End Region

#Region "ForumPost"

        Public Function TTTForum_GetPosts(ByVal threadID As Integer, ByVal threadPage As Integer, ByVal postsPerPage As Integer, ByVal flatView As Boolean, ByVal Filter As String) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {threadID, threadPage, postsPerPage, flatView, Filter})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_GetSinglePost(ByVal PostID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_AddPost( _
            ByVal ParentPostID As Integer, _
            ByVal ForumID As Integer, _
            ByVal UserID As Integer, _
            ByVal RemoteAddr As String, _
            ByVal Notify As Boolean, _
            ByVal Subject As String, _
            ByVal Body As String, _
            ByVal IsPinned As Boolean, _
            ByVal Image As String, _
            <SqlParameter(, , , , , ParameterDirection.Output)> ByVal PostId As Integer) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ParentPostID, ForumID, UserID, RemoteAddr, Notify, Subject, Body, IsPinned, Image, PostId})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                PostId = ConvertInteger(myCommand.Parameters("@PostID").Value)
            Catch Exc As System.Exception
                Dim strExc As String = Exc.Message
                Throw Exc
            End Try

            myConnection.Close()
            
			
            Return PostId

        End Function

        Public Function TTTForum_UpdatePost( _
            ByVal ThreadID As Integer, _
            ByVal PostID As Integer, _
            ByVal Notify As Boolean, _
            ByVal Subject As String, _
            ByVal Body As String, _
            ByVal IsPinned As Boolean, _
            ByVal Image As String, _
            ByVal LastModifiedAuthorID As Integer, _
            ByVal LastModifiedAuthor As String, _
            ByVal IsApproved As Boolean, _
            ByVal IsLocked As Boolean) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ThreadID, PostID, Notify, Subject, Body, IsPinned, Image, LastModifiedAuthorID, LastModifiedAuthor, IsApproved, IsLocked})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

        End Function

        Public Function TTTForum_GetSortOrderFromPost(ByVal postID As Integer, ByVal flatView As Boolean) As Integer
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_GetSortOrderFromPost", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@PostID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@FlatView", SqlDbType.Bit, 1)
            cmd.Parameters.Add("@SortOrder", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = postID
            cmd.Parameters(1).Value = flatView
            cmd.Parameters(2).Direction = ParameterDirection.Output

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Return ConvertInteger(cmd.Parameters(2).Value)

        End Function 'TTTForum_GetSortOrderFromPost


        Public Function TTTForum_GetPostFromThreadAndPage(ByVal threadID As Integer, ByVal threadPage As Integer, ByVal postsPerPage As Integer, ByVal flatView As Boolean) As Integer
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_GetPostFromThreadAndPage", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@ThreadPage", SqlDbType.Int, 4)
            cmd.Parameters.Add("@PostsPerPage", SqlDbType.Int, 4)
            cmd.Parameters.Add("@FlatView", SqlDbType.Bit, 1)
            cmd.Parameters.Add("@PostID", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = threadID
            cmd.Parameters(1).Value = threadPage
            cmd.Parameters(2).Value = postsPerPage
            cmd.Parameters(3).Value = flatView
            cmd.Parameters(4).Direction = ParameterDirection.Output

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Return ConvertInteger(cmd.Parameters(4).Value)

        End Function 'TTTForum_GetPostFromThreadAndPage

#End Region

#Region "ForumSearch"

        Private Function PopulateForumSearchInfo(ByVal dr As SqlDataReader) As ForumSearchInfo
            Dim forumSearchInfo As New forumSearchInfo()

            forumSearchInfo.Alias = ConvertString(dr("Alias"))
            forumSearchInfo.PostDate = ConvertDateTime(dr("PostDate"))
            forumSearchInfo.PostID = ConvertInteger(dr("PostID"))
            forumSearchInfo.ThreadID = ConvertInteger(dr("ThreadID"))
            forumSearchInfo.RecordCount = ConvertInteger(dr("RecordCount"))
            forumSearchInfo.Subject = ConvertString(dr("Subject"))
            forumSearchInfo.ForumID = ConvertString(dr("ForumID"))

            Return forumSearchInfo

        End Function 'PopulateForumSearchInfo

        Public Function TTTForum_SearchGetResults(ByVal searchObject As String, ByVal searchTerms As String, ByVal forumID As String, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal UserID As Integer, ByVal PortalID As Integer, Optional ByVal UserAlias As String = "", Optional ByVal StartDate As String = "", Optional ByVal EndDate As String = "") As ForumSearchInfoCollection
			Dim whereClause As String = ""
			Dim FirstRound As Boolean = False
            If searchTerms <> "" And searchObject <> "" Then
                whereClause = " AND ("
                whereClause += " Body LIKE '%" & searchTerms & "%' COLLATE French_CI_AI or "
                If searchTerms <> HtmlEncode(searchTerms) Then
                    whereClause += " Body LIKE '%" & HtmlEncode(searchTerms) & "%' COLLATE French_CI_AI or "
                End If
                If searchObject <> HtmlEncode(searchObject) Then
                    whereClause += " Subject LIKE '%" & HtmlEncode(searchObject) & "%' COLLATE French_CI_AI or "
                End If
                whereClause += " Subject LIKE '%" & searchObject & "%' COLLATE French_CI_AI "
                whereClause += ") "
            Else
                If searchObject <> "" Then
                    whereClause = " AND ("
                    If searchObject <> HtmlEncode(searchObject) Then
                        whereClause += " Subject LIKE '%" & HtmlEncode(searchObject) & "%' COLLATE French_CI_AI or "
                    End If
                    whereClause += " Subject LIKE '%" & searchObject & "%' COLLATE French_CI_AI "
                    whereClause += ") "
                End If
                If searchTerms <> "" Then
                    whereClause += " AND ("
                    If searchTerms <> HtmlEncode(searchTerms) Then
                        whereClause += " Body LIKE '%" & HtmlEncode(searchTerms) & "%' COLLATE French_CI_AI or"
                    End If
                    whereClause += " Body LIKE '%" & searchTerms & "%' COLLATE French_CI_AI "
                    whereClause += ") "
                End If
            End If

			
			If StartDate <> "" and EndDate <> "" then
			whereClause += " AND TTTForum_Posts.PostDate between '" + StartDate + "' and '" + EndDate + "' "
			end if
			
            ' Limit to just one forum if required
            If forumID <> "0" Then
			whereClause += " AND ( "
			Dim txtForum As String
            For Each txtForum In Split(forumID, ":", , CompareMethod.Text)
			if FirstRound then
			whereClause += "  or TTTForum_Threads.ForumID = " + txtForum + " "
			else
			whereClause += "     TTTForum_Threads.ForumID = " + txtForum + " "
			FirstRound = True
			end if
            Next
			whereClause += ")"
            End If
			
            If UserAlias <> "" then
			Dim dbForumUser As New ForumUserDB()
            Dim dru as SqlDataReader = dbForumUser.TTTForum_GetUsers(PortalId, UserAlias)
            If dru.Read Then
			whereClause += " AND ( TTTForum_Posts.UserID = " + dru("UserID").ToString
			While dru.Read()
          	whereClause += " or TTTForum_Posts.UserID = " + dru("UserID").ToString 
            End While
			whereClause += ")"
			End if
            dru.Close()			
			end if

			
		   ' write WhereClause
           ' Dim objStream As StreamWriter
            ' objStream = File.CreateText(httpContext.Current.Request.MapPath(glbPath + "database/search.log"))
           ' objStream.WriteLine(WhereClause + vbcrlf + forumID)
           ' objStream.Close()
			
			
            ' Execute stored procedure
            Dim conn As New SqlConnection(GetDBConnectionString)
            Dim cmd As New SqlCommand("TTTForum_SearchGetResults", conn)

            ' Populate parameters
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@WhereClause", SqlDbType.NVarChar, 3500)
            cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4)
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4)
            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4)
            cmd.Parameters.Add("@PortalID", SqlDbType.Int, 4)
            cmd.Parameters(0).Value = whereClause
            cmd.Parameters(1).Value = pageSize
            cmd.Parameters(2).Value = pageIndex
            cmd.Parameters(3).Value = UserID
            cmd.Parameters(4).Value = PortalID

            conn.Open()

            Dim forumSearchInfoCollection As New forumSearchInfoCollection()

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            While dr.Read()
                Dim forumSearchInfo As forumSearchInfo = PopulateForumSearchInfo(dr)
                forumSearchInfoCollection.Add(forumSearchInfo)
            End While

            dr.Close()
            conn.Close()

            Return forumSearchInfoCollection
        End Function 'TTTForum_GetForumSearchResults

#End Region


#Region "ForumModerate"

        Public Function TTTForum_Moderate_GetForums(ByVal UserID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_Moderate_GetPosts(ByVal ForumID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        'TTTForum_Moderate_ApprovePost
        Public Function TTTForum_Moderate_ApprovePost(ByVal PostID As Integer, ByVal ApprovedBy As Integer, ByVal Notes As String) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID, ApprovedBy, Notes})
            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try
            
        End Function

        Public Function TTTForum_Moderate_RejectPost(ByVal PostID As Integer, ByVal RejectedBy As Integer, ByVal Notes As String) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID, RejectedBy, Notes})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()

            Catch Exc As System.Exception
            End Try
            
        End Function

        Public Function TTTForum_Moderate_DeletePost(ByVal PostID As Integer, ByVal DeletedBy As Integer, ByVal Notes As String) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID, DeletedBy, Notes})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try
            
        End Function

        Public Function TTTForum_DeletePost(ByVal PostID As Integer, ByVal DeletedBy As Integer, ByVal Notes As String) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PostID, DeletedBy, Notes})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try
            
        End Function		
		
		
        Public Function TTTForum_Moderate_UserCreateUpdateDelete( _
                ByVal ForumID As Integer, _
                ByVal UserID As Integer, _
                ByVal EmailNotification As Boolean, _
                ByVal Action As Integer) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Adding: Action = 0
            'Update:  Action = 1
            'Delete:  Action = 2

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID, UserID, EmailNotification, Action})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
            Catch Exc As System.Exception
                Throw Exc
                ' duplicate group
            End Try
            myConnection.Close()

        End Function

        Public Function TTTForum_Moderate_GetForumModerator(ByVal ForumID As Integer, ByVal UserID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID, UserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

        Public Function TTTForum_Moderate_GetForumModerators(ByVal ForumID As Integer) As SqlDataReader
            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {ForumID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function

#End Region

#Region "Forum Statistics"

        Public Function TTTForum_Statistics_Get(ByVal PortalID As Integer, ByVal ModuleID As Integer, ByVal UpdateWindow As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, ModuleID, UpdateWindow})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

#End Region
    End Class 'ForumDB

#End Region

#Region "ForumUserDB"

    Public Class ForumUserDB

        Public Function TTTForum_GetUser(ByVal DNNUserID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_GetUsers(ByVal PortalID As Integer, ByVal Filter As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            If Filter Is Nothing Then Filter = ""

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {PortalID, Filter})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Sub TTTForum_UserCreateUpdateDelete( _
                    ByVal DNNUserID As Integer, _
                    ByVal [alias] As String, _
                    ByVal UseRichText As Boolean, _
                    ByVal UserAvatar As Boolean, _
                    ByVal Avatar As String, _
                    ByVal URL As String, _
                    ByVal Signature As String, _
                    ByVal TimeZone As Integer, _
                    ByVal Occupation As String, _
                    ByVal Interests As String, _
                    ByVal MSN As String, _
                    ByVal Yahoo As String, _
                    ByVal AIM As String, _
                    ByVal ICQ As String, _
                    ByVal Skin As String, _
                    ByVal IsTrusted As Boolean, _
                    ByVal EnableThreadTracking As Boolean, _
                    ByVal EnableDisplayUnreadThreadsOnly As Boolean, _
                    ByVal EnableDisplayInMemberList As Boolean, _
                    ByVal EnablePrivateMessages As Boolean, _
                    ByVal EnableOnlineStatus As Boolean, _
                    ByVal Action As Integer)

            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID, [alias], UseRichText, UserAvatar, Avatar, URL, Signature, TimeZone, Occupation, Interests, MSN, Yahoo, AIM, ICQ, Skin, IsTrusted, EnableThreadTracking, EnableDisplayUnreadThreadsOnly, EnableDisplayInMemberList, EnablePrivateMessages, EnableOnlineStatus, Action})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
				myConnection.Close()
            Catch Exc As System.Exception
                Dim strExc As String = Exc.ToString
                ' duplicate Forum
            End Try

            ForumUser.ResetForumUser(DNNUserID)

        End Sub

        Public Shared Function TTTForum_GetUserLastView(ByVal DNNUserID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Shared Sub TTTForum_UpdateUserForumsView(ByVal DNNUserID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

            ForumUser.ResetForumUser(DNNUserID)

        End Sub

        Public Shared Sub TTTForum_UpdateUserThreadView(ByVal DNNUserID As Integer)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

            ForumUser.ResetForumUser(DNNUserID)

        End Sub

        Public Shared Sub TTTForum_UpdateUserViewType(ByVal DNNUserID As Integer, ByVal FlatView As Boolean)
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DNNUserID, FlatView})

            Try
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            Catch Exc As System.Exception
            End Try

            ForumUser.ResetForumUser(DNNUserID)

        End Sub


        Public Function TTTForum_Moderate_GetForumsByUser(ByVal UserID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function TTTForum_GetUserFromAlias(ByVal [Alias] As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {[Alias]})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


    End Class 'ForumUserDB

#End Region


End Namespace