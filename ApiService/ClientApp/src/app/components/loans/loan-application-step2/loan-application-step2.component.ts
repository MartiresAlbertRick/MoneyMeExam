import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { Customer, Loan, Product } from 'src/app/entities/entities';
import { CustomerService, LoanService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-loan-application-step2',
  templateUrl: './loan-application-step2.component.html',
  styleUrls: ['./loan-application-step2.component.css']
})
export class LoanApplicationStep2Component implements OnInit {

  subscriptions$ : Subscription[] = [];
  loan : Loan = { };
  customer: Customer = { };
  products : Product[] = [];

  constructor(
    private loanService : LoanService,
    private productService : ProductService,
    private customerService : CustomerService,
    private route : ActivatedRoute,
    private router : Router
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
              //this.loadDropdowns();
            }
          }
        }
      ));
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.loanService.GetLoan<Loan>(this.loan.loanId),
        this.productService.GetProducts<Product[]>()
      ]).subscribe({
          next: (value1: [loans: Loan, products: Product[]]) => {
            this.loan = value1[0];
            this.products = value1[1];
            forkJoin([
              this.loanService.ComputeLoanRepayments<Loan>(JSON.stringify(this.loan)),
              this.customerService.GetCustomer<Customer>(this.loan.customerId)
            ]).subscribe({
              next: (value2: [loan: Loan, customer: Customer]) => {
                this.loan = value2[0];
                this.customer = value2[1];
              }, error : (error) => {
                alert('Caught an error. Please see logs.');
                console.log('loadData', error);
              }});
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadData', error);
          }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.loan.loanId == 0) {
        alert('Loan doesn\'t exist');
      } else {
        this.subscriptions$.push(
          forkJoin([
            this.loanService.UpdateLoan<Loan>(JSON.stringify(this.loan), true)
          ]).subscribe({
              next: (value: [loans: Loan]) => {
                this.loan = value[0];
                alert('Successfully saved!');
                this.router.navigate(['loan-application-completed']);
              }, error : (error) => {
                console.log('saveData', error);
                if (error.status == 400) {
                  this.router.navigate(['loan-application-denied']);
                } else {
                  alert('Caught an error. Please see logs.');
                }
              }}));
      }
    }
  }

}
