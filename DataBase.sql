USE [master]
GO

CREATE DATABASE [SM_DB]
GO

USE [SM_DB]
GO

CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado]) VALUES (1, N'119520621', N'SALAZAR MORALES GABRIEL', N'gsalazar20621@ufide.ac.cr', N'FNFQEG2k9/n4PZL9locOHA==', 1)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_CorreoElectronico] UNIQUE NONCLUSTERED 
(
	[CorreoElectronico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@Contrasenna  varchar(100),
    @Consecutivo  INT
AS
BEGIN

    UPDATE [dbo].[tUsuario]
    SET Contrasenna = @Contrasenna
    WHERE Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN

    SELECT  Consecutivo,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Estado
    FROM    tUsuario
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[RegistrarCuenta]
    @Identificacion     varchar(15),
    @Nombre             varchar(200),
    @CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN

    IF NOT EXISTS (
        SELECT 1 FROM tUsuario
        WHERE   Identificacion = @Identificacion
            OR  CorreoElectronico = @CorreoElectronico)
    BEGIN
	
        INSERT INTO dbo.tUsuario(Identificacion,Nombre,CorreoElectronico,Contrasenna,Estado)
        VALUES (@Identificacion, @Nombre, @CorreoElectronico,@Contrasenna,1)

    END
END
GO

CREATE PROCEDURE [dbo].[ValidarCorreo]
	@CorreoElectronico  varchar(100)
AS
BEGIN

    SELECT  Consecutivo,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Estado
    FROM    tUsuario
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Estado = 1

END
GO