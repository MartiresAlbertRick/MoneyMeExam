import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MobileNumber } from 'src/app/entities/entities';
import { MobileNumberService } from 'src/app/services/mobile-number.service';

@Component({
  selector: 'app-mobile-number-details',
  templateUrl: './mobile-number-details.component.html',
  styleUrls: ['./mobile-number-details.component.css']
})
export class MobileNumberDetailsComponent implements OnInit, OnDestroy {
  
  subscriptions$ : Subscription[] = [];
  mobileNumber : MobileNumber = { isBlackListed: false };

  constructor(
    private mobileNumberService : MobileNumberService,
    private route : ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscriptions$.push(
      this.route.params.subscribe(
        (params) => {
          let mobileNumberId = params['id'];
          if (mobileNumberId) {
            console.log('mobileNumberId found', mobileNumberId);
            this.mobileNumber.mobileNumberId = Number(mobileNumberId);
            if (this.mobileNumber.mobileNumberId > 0) {
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
      this.mobileNumberService
          .GetMobileNumber<MobileNumber>(this.mobileNumber.mobileNumberId)
          .subscribe({
            next: (mobileNumber: MobileNumber) => {
              this.mobileNumber = mobileNumber;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.mobileNumber.mobileNumberId == 0) {
        this.mobileNumber.mobileNumberId = undefined;
        this.subscriptions$.push(
          this.mobileNumberService
              .CreateMobileNumber<MobileNumber>(JSON.stringify(this.mobileNumber))
              .subscribe({
                next: (mobileNumber: MobileNumber) => {
                  this.mobileNumber = mobileNumber;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      } else {
        this.subscriptions$.push(
          this.mobileNumberService
              .UpdateMobileNumber<MobileNumber>(JSON.stringify(this.mobileNumber))
              .subscribe({
                next: (mobileNumber: MobileNumber) => {
                  this.mobileNumber = mobileNumber;
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
