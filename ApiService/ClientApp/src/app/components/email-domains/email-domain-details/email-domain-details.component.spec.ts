import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailDomainDetailsComponent } from './email-domain-details.component';

describe('EmailDomainDetailsComponent', () => {
  let component: EmailDomainDetailsComponent;
  let fixture: ComponentFixture<EmailDomainDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailDomainDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailDomainDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
