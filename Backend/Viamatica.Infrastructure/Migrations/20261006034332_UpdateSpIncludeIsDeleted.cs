using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viamatica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpIncludeIsDeleted : Migration
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

                    DECLARE @ClientId INT;
                    DECLARE @EndDate DATETIME;
                    DECLARE @MethodPaymentId INT;

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

                    UPDATE Contracts 
                    SET StatusContractStatusId = 'SUS' 
                    WHERE ContractId = @OldContractId;

                    INSERT INTO Contracts (
                        StartDate, 
                        EndDate, 
                        ServiceServiceId, 
                        StatusContractStatusId, 
                        ClientClientId, 
                        MethodPaymentMethodPaymentId, 
                        IsDeleted
                    )
                    VALUES (
                        GETUTCDATE(), 
                        @EndDate, 
                        @NewServiceId, 
                        'VIG', 
                        @ClientId, 
                        @MethodPaymentId, 
                        0
                    );
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
