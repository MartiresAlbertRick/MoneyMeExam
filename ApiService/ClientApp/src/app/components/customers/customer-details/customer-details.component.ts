import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Customer } from 'src/app/entities/entities';
import { CustomerService } from 'src/app/services/services';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  customer : Customer = { };

  constructor(
    private customerService : CustomerService,
    private route : ActivatedRoute,
    private datePipe : DatePipe
  ) { }

  ngOnInit(): void {
    this.subscriptions$.push(
      this.route.params.subscribe(
        (params) => {
          let customerId = params['id'];
          if (customerId) {
            console.log('customerId found', customerId);
            this.customer.customerId = Number(customerId);
            if (this.customer.customerId > 0) {
              this.loadData();
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
      this.customerService
          .GetCustomer<Customer>(this.customer.customerId)
          .subscribe({
            next: (customer: Customer) => {
              customer.dateOfBirth = this.datePipe.transform(customer.dateOfBirth, 'MM/dd/yyyy') || '';
              this.customer = customer;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.customer.customerId == 0) {
        this.customer.customerId = undefined;
        this.subscriptions$.push(
          this.customerService
              .CreateCustomer<Customer>(JSON.stringify(this.customer))
              .subscribe({
                next: (customer: Customer) => {
                  customer.dateOfBirth = this.datePipe.transform(customer.dateOfBirth, 'MM/dd/yyyy') || '';
                  this.customer = customer;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      } else {
        this.subscriptions$.push(
          this.customerService
              .UpdateCustomer<Customer>(JSON.stringify(this.customer))
              .subscribe({
                next: (customer: Customer) => {
                  customer.dateOfBirth = this.datePipe.transform(customer.dateOfBirth, 'MM/dd/yyyy') || '';
                  this.customer = customer;
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
