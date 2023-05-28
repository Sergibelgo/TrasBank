import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuSComponent } from './menu-s.component';

describe('MenuSComponent', () => {
  let component: MenuSComponent;
  let fixture: ComponentFixture<MenuSComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MenuSComponent]
    });
    fixture = TestBed.createComponent(MenuSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
