# ROADMAP

## Unique Accounts Bug
Users should be able to add an email (unique) OR a username for accounts. When a username only is used we have to enter a null or empty email into the database which fails the second time around due to the unique constraing on the column.

## Selection IDs for Accounts and Domains
When users attempt operations on accounts or domains we display their primary key to the console. If an account or domain is removed this leads to missing numbers (e.g. 1, 2, 5, 9). Decouple the user selection (UI) from the back-end (primary keys) so that we will always have a continuous set of numbers. 

