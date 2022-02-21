import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { Customer, EnumData, Loan, Product } from 'src/app/entities/entities';
import { CommonService, CustomerService, LoanService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-loan-details',
  templateUrl: './loan-details.component.html',
  styleUrls: ['./loan-details.component.css']
})
export class LoanDetailsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  loan : Loan = { };
  customers : Customer[] = [];
  products : Product[] = [];
  loanStatuses : EnumData[] = [];
  repaymentFrequencies : EnumData[] = [];

  constructor(
    private loanService : LoanService,
    private productService : ProductService,
    private customerService : CustomerService,
    private commonService : CommonService,
    private route : ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscriptions$.push(
      this.route.params.subscribe(
        (params) => {
          let loanId = params['id'];
          if (loanId) {
            console.log('loanId found', loanId);
            this.loan.loanId = Number(loanId);
            if (this.loan.loanId > 0) {
              this.loadData();
            } else {
              this.loadDropdowns();
            }
          }
        }
      ));
  }

  ngOnDestroy(): void {
    this.subscriptions$.forEach((subs) => {
      subs.unsubscribe();
    });
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.loanService.GetLoan<Loan>(this.loan.loanId),
        this.productService.GetProducts<Product[]>(),
        this.customerService.GetCustomers<Customer[]>(),
        this.commonService.GetLoanStatuses<EnumData[]>(),
        this.commonService.GetRepaymentFrequencies<EnumData[]>()
      ]).subscribe({
          next: (value: [loans: Loan, products: Product[], customers: Customer[], loanStatuses: EnumData[], repaymentFrequencies: EnumData[]]) => {
            this.loan = value[0];
            this.products = value[1];
            this.customers = value[2];
            this.loanStatuses = value[3];
            this.repaymentFrequencies = value[4];
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadData', error);
          }}));
  }

  loadDropdowns() {
    this.subscriptions$.push(
      forkJoin([
        this.productService.GetProducts<Product[]>(),
        this.customerService.GetCustomers<Customer[]>(),
        this.commonService.GetLoanStatuses<EnumData[]>(),
        this.commonService.GetRepaymentFrequencies<EnumData[]>()
      ]).subscribe({
          next: (value: [products: Product[], customers: Customer[], loanStatuses: EnumData[], repaymentFrequencies: EnumData[]]) => {
            this.products = value[0];
            this.customers = value[1];
            this.loanStatuses = value[2];
            this.repaymentFrequencies = value[3];
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadDropdowns', error);
          }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.loan.loanId == 0) {
        this.loan.loanId = undefined;
        this.subscriptions$.push(
          this.loanService
              .CreateLoan<Loan>(JSON.stringify(this.loan))
              .subscribe({
                next: (loan: Loan) => {
                  this.loan = loan;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      } else {
        this.subscriptions$.push(
          this.loanService
              .UpdateLoan<Loan>(JSON.stringify(this.loan))
              .subscribe({
                next: (loan: Loan) => {
                  this.loan = loan;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      }
    }
  }

}
