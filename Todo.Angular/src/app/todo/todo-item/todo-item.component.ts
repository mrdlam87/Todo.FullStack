import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';
import { TodoFormComponent } from '../todo-form/todo-form.component';
import { Todo } from '../todo.model';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss'],
})
export class TodoItemComponent implements OnInit {
  @Input() todo: Todo;
  todoForm: FormGroup;

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  onEditClicked() {
    this.userService.setCurrentUserTodo(this.todo);
    this.modalService.setModalContent({
      component: TodoFormComponent,
      data: {
        isEdit: true,
      },
    });
    this.modalService.setModalIsVisible(true);
  }

  onCheckClicked() {
    this.userService.setCurrentUserTodo(this.todo);
    this.userService.updateUserTodo({
      ...this.todo,
      complete: this.todoForm.value.complete,
    });
  }

  private initForm() {
    this.todoForm = new FormGroup({
      complete: new FormControl(this.todo.complete),
    });
  }
}
