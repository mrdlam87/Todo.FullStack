import { Component, OnInit } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';
import { UserFormComponent } from '../user-form/user-form.component';
import { User } from '../user.model';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss'],
})
export class UserCardComponent implements OnInit {
  users: User[] = [];

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.users = this.userService.getUsers();
  }

  addUser() {
    this.modalService.setModalContent({
      component: UserFormComponent,
      data: {
        isEdit: false,
      },
    });
    this.modalService.setModalIsVisible(true);
  }
}
