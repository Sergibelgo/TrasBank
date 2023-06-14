import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsInfoWorkerComponent } from './accounts-info-worker.component';

describe('AccountsInfoWorkerComponent', () => {
  let component: AccountsInfoWorkerComponent;
  let fixture: ComponentFixture<AccountsInfoWorkerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccountsInfoWorkerComponent]
    });
    fixture = TestBed.createComponent(AccountsInfoWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
