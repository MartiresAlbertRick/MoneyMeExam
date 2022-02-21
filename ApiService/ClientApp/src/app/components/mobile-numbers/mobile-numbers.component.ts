import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MobileNumber } from 'src/app/entities/mobile-number.interface';
import { MobileNumberService } from 'src/app/services/mobile-number.service';

@Component({
  selector: 'app-mobile-numbers',
  templateUrl: './mobile-numbers.component.html',
  styleUrls: ['./mobile-numbers.component.css']
})
export class MobileNumbersComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  mobileNumbers: MobileNumber[] = [];
  colHeaders: string[] = ["Mobile Number Id", "Number", "Is Blacklisted", "Actions"];

  constructor(
    private mobileNumberService : MobileNumberService
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
      this.mobileNumberService
          .GetMobileNumbers<MobileNumber[]>()
          .subscribe({
            next: (mobileNumbers: MobileNumber[]) => {
              this.mobileNumbers = mobileNumbers;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadData', error);
            }}));
  }
}
