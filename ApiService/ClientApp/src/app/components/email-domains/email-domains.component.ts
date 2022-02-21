import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { EmailDomain } from 'src/app/entities/entities';
import { EmailDomainService } from 'src/app/services/services';

@Component({
  selector: 'app-email-domains',
  templateUrl: './email-domains.component.html',
  styleUrls: ['./email-domains.component.css']
})
export class EmailDomainsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  emailDomains: EmailDomain[] = [];
  colHeaders: string[] = ["Email Domain Id", "Email Domain Name", "Is Blacklisted", "Actions"];

  constructor(
    private emailDomainService : EmailDomainService
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
      this.emailDomainService
          .GetEmailDomains<EmailDomain[]>()
          .subscribe({
            next: (emailDomains: EmailDomain[]) => {
              this.emailDomains = emailDomains;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }

}
