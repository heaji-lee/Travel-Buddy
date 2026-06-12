describe('Homepage', () => {
    it('should load successfully', () => {
        cy.visit('/');
        cy.get('body').should('be.visible');
    });
});

describe('Sidebar navigation', () => {
    it('should navigate to login page when clicking login', () => {
        cy.visit('/home');

        cy.get('[data-cy=login-button]').click();

        cy.url().should('include', '/login');
    });
});

describe('Auth state', () => {
    it('should show Login button when user is not logged in', () => {
        cy.clearLocalStorage();
        cy.clearCookies();

        cy.visit('/home');
        cy.contains('Login').should('be.visible');
    });

    it('should NOT show Login when user is logged in', () => {
        cy.window().then((win) => {
            win.localStorage.setItem('currentUserFullName', 'Helen');
            win.localStorage.setItem('currentUserEmail', 'helen@test.com');
        });
        cy.visit('/home');
        cy.contains('Login').should('not.exist');
    });
});
