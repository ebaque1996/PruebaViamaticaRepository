using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viamatica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContractSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE sp_RenewOrChangeContract
                    @OldContractId INT,
                    @NewServiceId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- 1. Declarar variables para heredar del contrato anterior
                    DECLARE @ClientId INT;
                    DECLARE @EndDate DATETIME;
                    DECLARE @MethodPaymentId INT;
                    
                    -- 2. Obtenemos los datos del contrato anterior
                    SELECT 
                        @ClientId = ClientClientId, 
                        @EndDate = EndDate,
                        @MethodPaymentId = MethodPaymentMethodPaymentId
                    FROM Contracts 
                    WHERE ContractId = @OldContractId;

                    IF @ClientId IS NULL
                    BEGIN
                        RAISERROR('El contrato anterior no existe.', 16, 1);
                        RETURN;
                    END

                    -- 3. Cambiamos el estado del contrato viejo a Sustituido (SUS)
                    UPDATE Contracts 
                    SET StatusContractStatusId = 'SUS' 
                    WHERE ContractId = @OldContractId;

                    -- 4. Creamos el nuevo contrato (VIG) manteniendo la fecha fin original y heredando el cliente y método de pago
                    INSERT INTO Contracts (
                        StartDate, 
                        EndDate, 
                        ServiceServiceId, 
                        StatusContractStatusId, 
                        ClientClientId, 
                        MethodPaymentMethodPaymentId
                        --, 
                        --IsDeleted
                    )
                    VALUES (
                        GETUTCDATE(), 
                        @EndDate, 
                        @NewServiceId, 
                        'VIG', 
                        @ClientId, 
                        @MethodPaymentId
                        --, 
                        --0
                    );
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_RenewOrChangeContract");
        }
    }
}
