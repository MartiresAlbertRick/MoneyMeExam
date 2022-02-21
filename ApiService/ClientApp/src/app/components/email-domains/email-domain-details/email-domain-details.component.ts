import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { EmailDomain } from 'src/app/entities/entities';
import { EmailDomainService } from 'src/app/services/services';

@Component({
  selector: 'app-email-domain-details',
  templateUrl: './email-domain-details.component.html',
  styleUrls: ['./email-domain-details.component.css']
})
export class EmailDomainDetailsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  emailDomain : EmailDomain = { isBlackListed: false };
  
  constructor(
    private emailDomainService : EmailDomainService,
    private route : ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscriptions$.push(
      this.route.params.subscribe(
        (params) => {
          let emailDomainId = params['id'];
          if (emailDomainId) {
            console.log('emailDomainId found', emailDomainId);
            this.emailDomain.emailDomainId = Number(emailDomainId);
            if (this.emailDomain.emailDomainId > 0) {
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
      this.emailDomainService
          .GetEmailDomain<EmailDomain>(this.emailDomain.emailDomainId)
          .subscribe({
            next: (emailDomain: EmailDomain) => {
              this.emailDomain = emailDomain;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.emailDomain.emailDomainId == 0) {
        this.emailDomain.emailDomainId = undefined;
        this.subscriptions$.push(
          this.emailDomainService
              .CreateEmailDomain<EmailDomain>(JSON.stringify(this.emailDomain))
              .subscribe({
                next: (emailDomain: EmailDomain) => {
                  this.emailDomain = emailDomain;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      } else {
        this.subscriptions$.push(
          this.emailDomainService
              .UpdateEmailDomain<EmailDomain>(JSON.stringify(this.emailDomain))
              .subscribe({
                next: (emailDomain: EmailDomain) => {
                  this.emailDomain = emailDomain;
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
