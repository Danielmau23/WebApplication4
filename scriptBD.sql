create database pruebaDotcreek

go 

USE [pruebaDotcreek]
GO
/****** Object:  Table [dbo].[plantillas]    Script Date: 27/10/2016 03:05:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[plantillas](
	[idPlantilla] [int] IDENTITY(1,1) NOT NULL,
	[mensaje] [varchar](50) NULL,
	[html] [varchar](50) NULL,
 CONSTRAINT [PK_plantillas] PRIMARY KEY CLUSTERED 
(
	[idPlantilla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[planUsu]    Script Date: 27/10/2016 03:05:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[planUsu](
	[usuario] [int] NULL,
	[plantilla] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 27/10/2016 03:05:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[contraseña] [varchar](50) NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[planUsu]  WITH CHECK ADD  CONSTRAINT [FK_planUsu_Plantillas] FOREIGN KEY([plantilla])
REFERENCES [dbo].[plantillas] ([idPlantilla])
GO
ALTER TABLE [dbo].[planUsu] CHECK CONSTRAINT [FK_planUsu_Plantillas]
GO
ALTER TABLE [dbo].[planUsu]  WITH CHECK ADD  CONSTRAINT [FK_planUsu_usuarios] FOREIGN KEY([usuario])
REFERENCES [dbo].[usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[planUsu] CHECK CONSTRAINT [FK_planUsu_usuarios]
GO
