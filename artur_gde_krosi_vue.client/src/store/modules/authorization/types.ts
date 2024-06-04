export interface isCorrectStatus {
    status: boolean | null,
    message: string
}

export interface IsCorrectLogIn {
    logLogin: isCorrectStatus,
    logPassword: isCorrectStatus,
}

export interface IsCorrectRegistration {
    regUsername: isCorrectStatus,
    regEmail: isCorrectStatus,
    regName: isCorrectStatus,
    regPassword: isCorrectStatus,
    regPasswordConfirmation: isCorrectStatus,
}

export interface LoginUserData {
    login: string,
    password: string,
    rememberUser: boolean
}

export interface RegistrationUserData {
    username: string,
    email: string,
    name: string,
    surname: string,
    patronymic: string,
    password: string,
    passwordConfirmation: string,
    emailNewsletter: boolean
}

export interface AuthorizationState {
    showLogInPopup: boolean,
    loginPopupMode: string,
    loginUserData: LoginUserData,
    registrationUserData: RegistrationUserData,
    isCorrectLogIn: IsCorrectLogIn,
    isCorrectRegistration: IsCorrectRegistration,
    succesfulyAuthorized: isCorrectStatus,
    succesfulyRegistered: isCorrectStatus,
}