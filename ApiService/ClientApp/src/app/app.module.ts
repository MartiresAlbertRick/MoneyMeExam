import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { MainComponent } from 'src/app/components/main/main.component';
import { LoansComponent } from 'src/app/components/loans/loans.component';
import { LoanApplicationStep1Component } from 'src/app/components/loans/loan-application-step1/loan-application-step1.component';
import { LoanApplicationStep2Component } from 'src/app/components/loans/loan-application-step2/loan-application-step2.component';
import { CustomersComponent } from 'src/app/components/customers/customers.component';
import { ProductsComponent } from 'src/app/components/products/products.component';
import { EmailDomainsComponent } from 'src/app/components/email-domains/email-domains.component';
import { EmailDomainDetailsComponent } from 'src/app/components/email-domains/email-domain-details/email-domain-details.component';
import { MobileNumbersComponent } from 'src/app/components/mobile-numbers/mobile-numbers.component';
import { MobileNumberDetailsComponent } from 'src/app/components/mobile-numbers/mobile-number-details/mobile-number-details.component';
import { ClientApiService, CommonService, CustomerService, EmailDomainService, LoanService, MobileNumberService, ProductService } from 'src/app/services/services';
import { HttpClientModule } from '@angular/common/http';
import { CustomerDetailsComponent } from './components/customers/customer-details/customer-details.component';
import { LoanDetailsComponent } from './components/loans/loan-details/loan-details.component';
import { ProductDetailsComponent } from './components/products/product-details/product-details.component';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { LoanApplicationCompletedComponent } from './components/loans/loan-application-completed/loan-application-completed.component';
import { LoanApplicationDeniedComponent } from './components/loans/loan-application-denied/loan-application-denied.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoansComponent,
    LoanDetailsComponent,
    LoanApplicationStep1Component,
    LoanApplicationStep2Component,
    CustomersComponent,
    CustomerDetailsComponent,
    ProductsComponent,
    ProductDetailsComponent,
    EmailDomainsComponent,
    EmailDomainDetailsComponent,
    MobileNumbersComponent,
    MobileNumberDetailsComponent,
    LoanApplicationCompletedComponent,
    LoanApplicationDeniedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ClientApiService,
    CustomerService,
    LoanService,
    ProductService,
    MobileNumberService,
    EmailDomainService,
    CommonService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
