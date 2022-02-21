import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { LoansComponent } from './components/loans/loans.component';
import { LoanApplicationStep1Component } from './components/loans/loan-application-step1/loan-application-step1.component';
import { LoanApplicationStep2Component } from './components/loans/loan-application-step2/loan-application-step2.component';
import { CustomersComponent } from './components/customers/customers.component';
import { ProductsComponent } from './components/products/products.component';
import { EmailDomainsComponent } from './components/email-domains/email-domains.component';
import { EmailDomainDetailsComponent } from './components/email-domains/email-domain-details/email-domain-details.component';
import { MobileNumbersComponent } from './components/mobile-numbers/mobile-numbers.component';
import { MobileNumberDetailsComponent } from './components/mobile-numbers/mobile-number-details/mobile-number-details.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoansComponent,
    LoanApplicationStep1Component,
    LoanApplicationStep2Component,
    CustomersComponent,
    ProductsComponent,
    EmailDomainsComponent,
    EmailDomainDetailsComponent,
    MobileNumbersComponent,
    MobileNumberDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
