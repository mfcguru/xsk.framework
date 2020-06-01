UPDATE [dbo].[TeamMember] SET [RgbLookupId] = NULL

DELETE FROM [dbo].[RgbLookup]

USE [Xsk]
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (1, N'rgb(139,0,139)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (2, N'rgb(100,149,237)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (3, N'rgb(220,20,60)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (4, N'rgb(192,192,192)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (5, N'rgb(0,128,128)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (6, N'rgb(128,0,0)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (7, N'rgb(128,128,128)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (8, N'rgb(220,20,60)')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (9, N'rgb(0,0,255)
')
GO
INSERT [dbo].[RgbLookup] ([RgbLookupId], [RgbLookupValue]) VALUES (10, N'rgb(0,128,0)
')
GO
