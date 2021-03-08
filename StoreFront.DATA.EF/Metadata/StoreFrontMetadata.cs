using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF
{
    #region Category Metadata

    public class CategoryMetadata
    {
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "*Category name required")]
        [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]

    public partial class Category
    {

    }

    #endregion

    #region Department Metadata

    public class DepartmentMetadata
    {
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "*Department name required")]
        [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]

    public partial class Department
    {

    }

    #endregion

    #region Employee Metadata

    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "*Last MachineTypeName Required")]
        [StringLength(20, ErrorMessage = "* Last MachineTypeName must be 20 characters or less")]
        [Display(Name = "Last MachineTypeName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*First MachineTypeName Required")]
        [StringLength(15, ErrorMessage = "*First MachineTypeName must be 20 characters or less")]
        [Display(Name = "First MachineTypeName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Department Required")]
        public int DepartmentID { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(30, ErrorMessage = "*Title must be 30 characters or less")]
        public string Title { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "[N/A]")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "[N/A]")]
        public Nullable<System.DateTime> HireDate { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(30, ErrorMessage = "*Address must be 30 characters or less")]
        public string Address { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(30, ErrorMessage = "*City must be 15 characters or less")]
        public string City { get; set; }

        [Display(Name = "State")]
        public Nullable<int> StateID { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(5, ErrorMessage = "*Zip Code MachineTypeName must be 5 characters or less")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(15, ErrorMessage = "*Phone Number must be 15 characters or less")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Reports To")]
        public Nullable<int> ReportsToID { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]

    public partial class Employee
    {
        [Display(Name = "Employee")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }

    #endregion

    #region Machine Type Metadata

    public class MachineTypeMetadata
    {
        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "*Department name required")]
        [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
        public string MachineTypeName { get; set; }
    }

    [MetadataType(typeof(MachineTypeMetadata))]

    public partial class MachineType
    {

    }

    #endregion

    #region Manufacture Metadata

    public class ManufactureMetadata
    {
        [Display(Name = "Manufacture")]
        [Required(ErrorMessage = "*Manufacture name required")]
        [StringLength(50, ErrorMessage = "*Must be 50 characteres or less")]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage = "*City name required")]
        [StringLength(15, ErrorMessage = "*City must be 15 characters or less")]
        public string City { get; set; }

        //public Nullable<int> StateID { get; set; }

        [Required(ErrorMessage = "*Country name required")]
        [StringLength(15, ErrorMessage = "*Country must be 15 characters or less")]
        public string Country { get; set; }
    }

    [MetadataType(typeof(ManufactureMetadata))]

    public partial class Manufacture
    {

    }


    #endregion

    #region MaskSize Metadata

    public class MaskSizeMetadata
    {
        [Display(Name = "Mask Size")]
        [Required(ErrorMessage = "*Mask size name required")]
        [StringLength(50, ErrorMessage = "*Must be 50 characteres or less")]
        public string Size { get; set; }
    }

    [MetadataType(typeof(MaskSizeMetadata))]

    public partial class MaskSize
    {

    }

    #endregion

    #region MaskType Metadata

    public class MaskTypeMetadata
    {
        [Display(Name = "Mask Type")]
        [Required(ErrorMessage = "*Mask type requreid")]
        [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
        public string MaskTypeName { get; set; }
    }

    [MetadataType(typeof(MaskTypeMetadata))]

    public partial class MaskType
    {

    }

    #endregion

    #region Product Metadata

    public class ProductMetadata
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "*Product name required")]
        [StringLength(100, ErrorMessage = "*Must be 100 characteres or less")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(800, ErrorMessage = "*Must be 800 characteres or less")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Image")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(100, ErrorMessage = "*Must be 100 characteres or less")]
        public string ProductImage { get; set; }

        [Required(ErrorMessage = "*Category required")]
        public int CategoryID { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "*Price required")]
        public decimal Price { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        public Nullable<int> MaskSizeID { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        public Nullable<int> MaskTypeID { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        public Nullable<int> MachineTypeID { get; set; }

        [Display(Name = "Stock Status")]
        [Required(ErrorMessage = "*Stock Status required")]
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "*Manufacturer required")]
        public int ManufacturerID { get; set; }

        [Display(Name = "Units Available")]
        [Required(ErrorMessage = "*Units available required")]
        public int UnitsAvailable { get; set; }

        [Display(Name = "Featured Product")]
        [Required(ErrorMessage = "*required")]
        public bool IsFeatured { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]

    public partial class Product
    {

    }

    #endregion

    #region State Metadata

    public class StateMetadata
    {
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "State required")]
        [Display(Name = "State")]
        public string StateName { get; set; }
    }

    [MetadataType(typeof(StateMetadata))]

    public partial class State
    {

    }

    #endregion

    #region StockStatusMetadata

    public class StockStatusMetadata
    {
        [Display(Name = "Stock Status")]
        [Required(ErrorMessage = "*Stock Status required")]
        [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
        public string StockStatus { get; set; }
    }

    [MetadataType(typeof(StockStatusMetadata))]

    public partial class StockStatu
    {

    }

    #endregion


    //#region Category Metadata

    //public class CategoryMetaData
    //{
    //    [Display(MachineTypeName = "Category")]
    //    [Required(ErrorMessage = "*Category required")]
    //    [StringLength(25, ErrorMessage = "*Must be 25 characteres or less")]
    //    public string CategoryName { get; set; }
    //}

    //[MetadataType(typeof(CategoryMetaData))]

    //public partial class Category
    //{

    //}

    //#endregion

    //#region Department Metadata

    //public class DepartmentMetaData
    //{
    //    [Display(MachineTypeName = "Department")]
    //    [Required(ErrorMessage = "*Department required")]
    //    [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
    //    public string DepartmentName { get; set; }
    //}

    //[MetadataType(typeof(DepartmentMetaData))]

    //public partial class Deparatment
    //{

    //}

    //#endregion

    //#region EmployeeMetadata

    //public class EmployeeMetaData
    //{
    //    [Required(ErrorMessage = "*Last MachineTypeName Required")]
    //    [StringLength(20, ErrorMessage = "* Last MachineTypeName must be 20 characters or less")]
    //    [Display(MachineTypeName = "Last MachineTypeName")]
    //    public string LastName { get; set; }

    //    [Required(ErrorMessage = "*First MachineTypeName Required")]
    //    [StringLength(15, ErrorMessage = "*First MachineTypeName must be 20 characters or less")]
    //    [Display(MachineTypeName = "First MachineTypeName")]
    //    public string FirstName { get; set; }

    //    [Required(ErrorMessage = "*Department Required")]
    //    public int DepartmenetID { get; set; }

    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    [StringLength(30, ErrorMessage = "*Title must be 30 characters or less")]
    //    public string Title { get; set; }

    //    [Display(MachineTypeName = "Date of Birth")]
    //    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "[N/A]")]
    //    public Nullable<System.DateTime> BirthDate { get; set; }

    //    [Display(MachineTypeName = "Hire Date")]
    //    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "[N/A]")]
    //    public Nullable<System.DateTime> HireDate { get; set; }

    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    [StringLength(30, ErrorMessage = "*Address must be 30 characters or less")]
    //    public string Address { get; set; }

    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    [StringLength(30, ErrorMessage = "*City must be 15 characters or less")]
    //    public string City { get; set; }

    //    public Nullable<int> StateID { get; set; }

    //    [Display(MachineTypeName = "Postal Code")]
    //    [StringLength(5, ErrorMessage = "*Zip Code MachineTypeName must be 5 characters or less")]
    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    public string PostalCode { get; set; }

    //    [Display(MachineTypeName = "Phone Number")]
    //    [StringLength(15, ErrorMessage = "*Phone Number must be 15 characters or less")]
    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    public string PhoneNumber { get; set; }

    //    public Nullable<int> ReportsToID { get; set; }
    //}

    //[MetadataType(typeof(EmployeeMetaData))]

    //public partial class Employee
    //{
    //    [Display(MachineTypeName = "Employee")]
    //    public string FullName
    //    {
    //        get { return $"{FirstName} {LastName}"; }
    //    }
    //}

    //#endregion

    //#region MachineTypeMetaData

    //public class MachineTypeMetaData
    //{
    //    [Display(MachineTypeName = "Machine Type")]
    //    [Required(ErrorMessage = "*Machine Type required")]
    //    [StringLength(20, ErrorMessage = "*Must be 20 characteres or less")]
    //    public string MachineTypeName { get; set; }
    //}

    //[MetadataType(typeof(MachineTypeMetaData))]

    //public partial class MachineType
    //{

    //}

    //#endregion

    //#region Manufacture Metadata

    //public class ManufactureMetaData
    //{
    //    [Display(MachineTypeName = "Manufacture")]
    //    [Required(ErrorMessage = "*Manufacture name required")]
    //    [StringLength(50, ErrorMessage = "*Must be 50 characteres or less")]
    //    public string ManufactureName { get; set; }

    //    [Required(ErrorMessage = "*City name required")]
    //    [StringLength(15, ErrorMessage = "*City must be 15 characters or less")]
    //    public string City { get; set; }

    //    //public Nullable<int> StateID { get; set; }

    //    [Required(ErrorMessage = "*Country name required")]
    //    [StringLength(15, ErrorMessage = "*Country must be 15 characters or less")]
    //    public string Country { get; set; }
    //}

    //[MetadataType(typeof(ManufactureMetaData))]

    //public partial class Manufacture
    //{

    //}

    //#endregion

    //#region Mask Size Metadata

    //public class MaskSizeMetaData
    //{
    //    [Display(MachineTypeName = "Mask Size")]
    //    [Required(ErrorMessage = "*Mask Size required")]
    //    [StringLength(20, ErrorMessage = "*Mask size must be 20 characteres or less")]
    //    public string Size { get; set; }
    //}

    //[MetadataType(typeof(MaskSizeMetaData))]

    //public partial class MaskSize
    //{

    //}

    //#endregion

    //#region Mask Type Metadata

    //public class MaskTypeMetaData
    //{
    //    [Display(MachineTypeName = "Mask Type")]
    //    [Required(ErrorMessage = "*Mask Type required")]
    //    [StringLength(20, ErrorMessage = "*Mask type must be 20 characteres or less")]
    //    public string MaskTypeName { get; set; }
    //}

    //[MetadataType(typeof(MaskTypeMetaData))]

    //public partial class MaskType
    //{

    //}

    //#endregion

    //#region Product Metadata

    //public class ProductMetaData
    //{
    //    [Display(MachineTypeName = "Product MachineTypeName")]
    //    [Required(ErrorMessage = "*Product name required")]
    //    [StringLength(50, ErrorMessage = "*Mask size must be 50 characteres or less")]
    //    public string MachineTypeName { get; set; }

    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    [StringLength(200, ErrorMessage = "*Description must be 200 characters or less")]
    //    [UIHint("MultililneText")]
    //    public string Description { get; set; }

    //    [Display(MachineTypeName = "Machine Type")]
    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    public Nullable<int> MachineTypeID { get; set; }

    //    [Display(MachineTypeName = "Mask Type")]
    //    [DisplayFormat(NullDisplayText = "[N/A]")]
    //    public Nullable<int> MaskTypeID { get; set; }

    //    [Required(ErrorMessage = "*Manufacture required")]
    //    [Display(MachineTypeName= "Manufacture")]
    //    public int ManufactuterID { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "* Value must be a valid number 0 or larger")]
    //    [DisplayFormat(DataFormatString = "{0:c}")]
    //    [Required(ErrorMessage = "*Price required")]
    //    public decimal Price { get; set; }

    //    [Required(ErrorMessage = "*Stock status required")]
    //    [Display(MachineTypeName = "Stock Status")]
    //    public int StockStatusID { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "* Value must be a valid number 0 or larger")]
    //    [Required(ErrorMessage = "*Units available required")]
    //    [Display(MachineTypeName ="Units Available")]
    //    public int UnitsAvailable { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "* Value must be a valid number 0 or larger")]
    //    [Required(ErrorMessage = "*Units on order required")]
    //    [Display(MachineTypeName = "Units On Order")]
    //    public int UnitsOnOrder { get; set; }

    //    [Display(MachineTypeName = "Product Category")]
    //    [Required(ErrorMessage = "Category required")]
    //    public int CategoryID { get; set; }

    //    //public string ImageUrl { get; set; }

    //    [Display(MachineTypeName = "Mask Size")]
    //    public Nullable<int> MaskSizeID { get; set; }
    //}

    //[MetadataType(typeof(ProductMetaData))]

    //public partial class Product
    //{

    //}

    //#endregion

    //#region State Metadata

    //public class StateMetaData
    //{
    //    [Required(ErrorMessage = "State required")]
    //    [Display(MachineTypeName = "State")]
    //    public string StateName { get; set; }
    //}

    //[MetadataType(typeof(StateMetaData))]

    //public partial class State
    //{

    //}
    //#endregion

    //#region Stock Status Metadata

    //public class StockStatusMetaData
    //{
    //    [Required(ErrorMessage = "Stock Status required")]
    //    [Display(MachineTypeName = "Stock Staus")]
    //    public string StockStatusName { get; set; }
    //}

    //[MetadataType(typeof(StockStatusMetaData))]

    //public partial class StockStatu
    //{

    //}

    //#endregion
}
