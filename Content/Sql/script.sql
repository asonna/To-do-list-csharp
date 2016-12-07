USE [ToDo]
GO
/****** Object:  Table [dbo].[tasks]    Script Date: 12/6/2016 5:54:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([id], [description]) VALUES (1, N'Wash the car')
INSERT [dbo].[tasks] ([id], [description]) VALUES (2, N'Walk the dog')
INSERT [dbo].[tasks] ([id], [description]) VALUES (3, N'Prepare dinner')
SET IDENTITY_INSERT [dbo].[tasks] OFF
