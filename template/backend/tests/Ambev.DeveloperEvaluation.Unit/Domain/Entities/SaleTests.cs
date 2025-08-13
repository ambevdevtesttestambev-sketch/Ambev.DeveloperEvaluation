using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

    public class SaleTests
    {
        [Fact]
        public void Sale_DefaultConstructor_InitializesProperties()
        {
            // Act
            var sale = new Sale();

            // Assert
            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.NotNull(sale.Items);
            Assert.Empty(sale.Items);
            Assert.False(sale.IsCancelled);
            Assert.Equal(0, sale.TotalAmount);
        }

        [Fact]
        public void Sale_SetProperties_PropertiesAreSetCorrectly()
        {
            // Arrange
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "S123",
                SaleDate = new DateTime(2024, 6, 1),
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                TotalAmount = 100.50m,
                IsCancelled = true,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10.0m,
                        Discount = 1.0m,
                        TotalAmount = 19.0m
                    }
                }
            };

            // Assert
            Assert.Equal("S123", sale.SaleNumber);
            Assert.Equal(new DateTime(2024, 6, 1), sale.SaleDate);
            Assert.Equal("John Doe", sale.CustomerName);
            Assert.Equal("Main Branch", sale.BranchName);
            Assert.Equal(100.50m, sale.TotalAmount);
            Assert.True(sale.IsCancelled);
            Assert.Single(sale.Items);
            Assert.Equal("Product A", sale.Items[0].ProductName);
        }

        [Fact]
        public void Sale_AddSaleItem_ItemIsAdded()
        {
            // Arrange
            var sale = new Sale();
            var item = new SaleItem
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product B",
                Quantity = 1,
                UnitPrice = 20.0m,
                Discount = 0.0m,
                TotalAmount = 20.0m
            };

            // Act
            sale.Items.Add(item);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal("Product B", sale.Items[0].ProductName);
        }
    }

