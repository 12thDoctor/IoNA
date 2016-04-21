Public Class Rootobject
    Public Property kind As String
    Public Property data As Data
End Class

Public Class Data
    Public Property facets As Facets
    Public Property modhash As String
    Public Property children() As Submission()
    Public Property after As String
    Public Property before As Object
End Class

Public Class Facets
End Class

Public Class Submission
    Public Property kind As String
    Public Property data As SubmissionInfo
End Class

Public Class SubmissionInfo
    Public Property domain As String
    Public Property banned_by As Object
    Public Property media_embed As Media_Embed
    Public Property subreddit As String
    Public Property selftext_html As Object
    Public Property selftext As String
    Public Property likes As Object
    Public Property suggested_sort As String
    Public Property user_reports() As Object()
    Public Property secure_media As Object
    Public Property link_flair_text As String
    Public Property id As String
    Public Property from_kind As Object
    Public Property gilded As Integer
    Public Property archived As Boolean
    Public Property clicked As Boolean
    Public Property report_reasons As Object
    Public Property author As String
    Public Property media As Object
    Public Property score As Integer
    Public Property approved_by As Object
    Public Property over_18 As Boolean
    Public Property hidden As Boolean
    Public Property num_comments As Integer
    Public Property thumbnail As String
    Public Property subreddit_id As String
    Public Property hide_score As Boolean
    Public Property edited As Boolean
    Public Property link_flair_css_class As String
    Public Property author_flair_css_class As String
    Public Property downs As Integer
    Public Property secure_media_embed As Secure_Media_Embed
    Public Property saved As Boolean
    Public Property removal_reason As Object
    Public Property stickied As Boolean
    Public Property from As Object
    Public Property is_self As Boolean
    Public Property from_id As Object
    Public Property permalink As String
    Public Property locked As Boolean
    Public Property name As String
    Public Property created As Single
    Public Property url As String
    Public Property author_flair_text As String
    Public Property quarantine As Boolean
    Public Property title As String
    Public Property created_utc As Single
    Public Property distinguished As Object
    Public Property mod_reports() As Object()
    Public Property visited As Boolean
    Public Property num_reports As Object
    Public Property ups As Integer
    Public Property preview As Preview
    Public Property post_hint As String
End Class

Public Class Media_Embed
End Class

Public Class Secure_Media_Embed
End Class

Public Class Preview
    Public Property images() As ImageInfo()
End Class

Public Class ImageInfo
    Public Property source As SourceInfo
    Public Property resolutions() As ResolutionInfo()
    Public Property variants As Variants
    Public Property id As String
End Class

Public Class SourceInfo
    Public Property url As String
    Public Property width As Integer
    Public Property height As Integer
End Class

Public Class Variants
End Class

Public Class ResolutionInfo
    Public Property url As String
    Public Property width As Integer
    Public Property height As Integer
End Class

