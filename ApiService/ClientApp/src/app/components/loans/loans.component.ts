import { Component, OnDestroy, OnInit } from '@angular/core';
import { forkJoin, Subscription } from 'rxjs';
import { Customer, EnumData, Loan, Product } from 'src/app/entities/entities';
import { CommonService, CustomerService, LoanService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css']
})
export class LoansComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  loans : Loan[] = [];
  customers : Customer[] = [];
  products : Product[] = [];
  loanStatuses : EnumData[] = [];
  repaymentFrequencies : EnumData[] = [];
  colHeaders: string[] = ["Loan Id", "Customer", "Product", "Loan Amount", "Repayment Terms", "Repayment Frequency", "Loan Status", "Actions"];

  constructor(
    private loanService : LoanService,
    private productService : ProductService,
    private customerService : CustomerService,
    private commonService : CommonService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.subscriptions$.forEach((subs) => {
      subs.unsubscribe();
    })
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.loanService.GetLoans<Loan[]>(),
        this.productService.GetProducts<Product[]>(),
        this.customerService.GetCustomers<Customer[]>(),
        this.commonService.GetLoanStatuses<EnumData[]>(),
        this.commonService.GetRepaymentFrequencies<EnumData[]>()
      ]).subscribe({
          next: (value: [loans: Loan[], products: Product[], customers: Customer[], loanStatuses: EnumData[], repaymentFrequencies: EnumData[]]) => {
            this.loans = value[0];
            this.products = value[1];
            this.customers = value[2];
            this.loanStatuses = value[3];
            this.repaymentFrequencies = value[4];
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadData', error);
          }}));
  }

  getCustomerName(customerId ?: number) : string {
    let result = '';
    this.customers.forEach((customer) => {
      if (customer.customerId == customerId) {
        result = customer.firstName + ' ' + customer.lastName;
      }
    });
    return result;
  }

  getProductName(productId ?: number) : string {
    let result = '';
    this.products.forEach((product) => {
      if (product.productId == productId) {
        result = product.productName || '';
      }
    });
    return result;
  }

  getRepaymentFrequencyDescription(id?: number) : string {
    let result = '';
    this.repaymentFrequencies.forEach((item) => {
      if (item.id == id) {
        result = item.description || '';
      }
    });
    return result;
  }

  getLoanStatusDescription(id?: number) : string {
    let result = '';
    this.loanStatuses.forEach((item) => {
      if (item.id == id) {
        result = item.description || '';
      }
    });
    return result;
  }
}
