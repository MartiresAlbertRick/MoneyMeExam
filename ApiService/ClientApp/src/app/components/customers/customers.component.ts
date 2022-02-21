import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Customer } from 'src/app/entities/customer.interface';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  customers: Customer[] = [];
  colHeaders: string[] = ["Customer Id", "Customer Name", "Salutation", "Date of Birth", "Mobile", "Email", "Actions"];

  constructor(
    private customerService : CustomerService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.subscriptions$.forEach((subs) => {
      subs.unsubscribe();
    });
  }

  loadData() {
    this.subscriptions$.push(
      this.customerService
          .GetCustomers<Customer[]>()
          .subscribe({
            next: (customers: Customer[]) => {
              this.customers = customers;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }

}
