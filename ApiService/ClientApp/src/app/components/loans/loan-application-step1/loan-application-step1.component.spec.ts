import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanApplicationStep1Component } from './loan-application-step1.component';

describe('LoanApplicationStep1Component', () => {
  let component: LoanApplicationStep1Component;
  let fixture: ComponentFixture<LoanApplicationStep1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoanApplicationStep1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanApplicationStep1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
