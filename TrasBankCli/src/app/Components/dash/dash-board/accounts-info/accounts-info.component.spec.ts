import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsInfoComponent } from './accounts-info.component';

describe('AccountsInfoComponent', () => {
  let component: AccountsInfoComponent;
  let fixture: ComponentFixture<AccountsInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccountsInfoComponent]
    });
    fixture = TestBed.createComponent(AccountsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
