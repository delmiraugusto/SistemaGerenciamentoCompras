export class User {
  id: number = -1;
  user: string = "";
  password: string = "";
  roleID?: number;
}

export class UserLogin {
  email: string = "";
  password: string = "";
}

export class UserLoginResponse {
  name: string = "";
  token: string = "";
  roleID?: number;
}

export class UserInsert {
  email: string = "";
  name: string = "";
  password: string = "";
  roleID?: number;
}

export class UserUpdate {
  email: string = "";
  name: string = "";
  password: string = "";
  roleID?: number;
}