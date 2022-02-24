import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanApplicationCompletedComponent } from './loan-application-completed.component';

describe('LoanApplicationCompletedComponent', () => {
  let component: LoanApplicationCompletedComponent;
  let fixture: ComponentFixture<LoanApplicationCompletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoanApplicationCompletedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanApplicationCompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
