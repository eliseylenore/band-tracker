USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 3/3/2017 4:18:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 3/3/2017 4:18:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[band_id] [int] NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 3/3/2017 4:18:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (1, N'Hello')
INSERT [dbo].[bands] ([id], [name]) VALUES (2, N'Hello')
INSERT [dbo].[bands] ([id], [name]) VALUES (3, N'Hello')
SET IDENTITY_INSERT [dbo].[bands] OFF
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (1, 29)
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (2, 29)
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (2, 29)
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (1, 29)
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (1, 29)
INSERT [dbo].[bands_venues] ([band_id], [venue_id]) VALUES (2, 29)
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (5, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (11, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (13, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (25, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (26, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (27, N'')
INSERT [dbo].[venues] ([id], [name]) VALUES (29, N'Pirate''s Booty Ale House')
INSERT [dbo].[venues] ([id], [name]) VALUES (19, N'')
SET IDENTITY_INSERT [dbo].[venues] OFF
