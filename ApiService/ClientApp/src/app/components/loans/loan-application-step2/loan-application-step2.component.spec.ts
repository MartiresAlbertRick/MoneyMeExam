import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanApplicationStep2Component } from './loan-application-step2.component';

describe('LoanApplicationStep2Component', () => {
  let component: LoanApplicationStep2Component;
  let fixture: ComponentFixture<LoanApplicationStep2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoanApplicationStep2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanApplicationStep2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
