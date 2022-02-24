import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDetailsComponent } from 'src/app/components/customers/customer-details/customer-details.component';
import { CustomersComponent } from 'src/app/components/customers/customers.component';
import { EmailDomainDetailsComponent } from 'src/app/components/email-domains/email-domain-details/email-domain-details.component';
import { EmailDomainsComponent } from 'src/app/components/email-domains/email-domains.component';
import { LoanApplicationStep1Component } from 'src/app/components/loans/loan-application-step1/loan-application-step1.component';
import { LoanApplicationStep2Component } from 'src/app/components/loans/loan-application-step2/loan-application-step2.component';
import { LoanDetailsComponent } from 'src/app/components/loans/loan-details/loan-details.component';
import { LoansComponent } from 'src/app/components/loans/loans.component';
import { MainComponent } from 'src/app/components/main/main.component';
import { MobileNumberDetailsComponent } from 'src/app/components/mobile-numbers/mobile-number-details/mobile-number-details.component';
import { MobileNumbersComponent } from 'src/app/components/mobile-numbers/mobile-numbers.component';
import { ProductDetailsComponent } from 'src/app/components/products/product-details/product-details.component';
import { ProductsComponent } from 'src/app/components/products/products.component';
import { LoanApplicationCompletedComponent } from './components/loans/loan-application-completed/loan-application-completed.component';
import { LoanApplicationDeniedComponent } from './components/loans/loan-application-denied/loan-application-denied.component';

const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'main' , component: MainComponent,
     children: [
       { path: 'customers', component: CustomersComponent },
       { path: 'customers/:id', component: CustomerDetailsComponent },
       { path: 'loans', component: LoansComponent },
       { path: 'loans/:id', component: LoanDetailsComponent },
       { path: 'products', component: ProductsComponent },
       { path: 'products/:id', component: ProductDetailsComponent },
       { path: 'email-domains', component: EmailDomainsComponent },
       { path: 'email-domains/:id', component: EmailDomainDetailsComponent },
       { path: 'mobile-numbers', component: MobileNumbersComponent },
       { path: 'mobile-numbers/:id', component: MobileNumberDetailsComponent },
     ]},
  { path: 'loan-application-step-1/:id', component: LoanApplicationStep1Component },
  { path: 'loan-application-step-2/:id', component: LoanApplicationStep2Component },
  { path: 'loan-application-completed', component: LoanApplicationCompletedComponent },
  { path: 'loan-application-denied', component: LoanApplicationDeniedComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
