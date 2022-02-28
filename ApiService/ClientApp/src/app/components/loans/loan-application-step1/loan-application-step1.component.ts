import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { Customer, Loan, Product } from 'src/app/entities/entities';
import { CustomerService, LoanService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-loan-application-step1',
  templateUrl: './loan-application-step1.component.html',
  styleUrls: ['./loan-application-step1.component.css']
})
export class LoanApplicationStep1Component implements OnInit {

  subscriptions$ : Subscription[] = [];
  loan : Loan = { };
  customer: Customer = { };
  customers : Customer[] = [];
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
            } 
          }
        }
      ));
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.loanService.GetLoan<Loan>(this.loan.loanId),
        this.productService.GetProducts<Product[]>(),
        this.customerService.GetCustomers<Customer[]>()
      ]).subscribe({
          next: (value: [loans: Loan, products: Product[], customers: Customer[]]) => {
            this.loan = value[0];
            this.products = value[1];
            this.customers = value[2];
            this.customers.forEach((customer) => {
              if (customer.customerId == this.loan.customerId) {
                this.customer = customer;
              }
            });
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
            this.loanService.UpdateLoan<Loan>(JSON.stringify(this.loan)),
            this.customerService.UpdateCustomer<Customer>(JSON.stringify(this.customer))
          ]).subscribe({
              next: (value: [loans: Loan, customer: Customer]) => {
                this.loan = value[0];
                this.customer = value[1];
                alert('Successfully saved!');
                this.router.navigate([`loan-application-step-2/${this.loan.loanId}`]);
              }, error : (error) => {
                alert('Caught an error. Please see logs.');
                console.log('saveData', error);
              }}));
      }
    }
  }

}
