
<template>
    <div class="auth register">
        <h1 class="logo"><span>Book</span>Shop</h1>
        <div class="title-wrapper">
            <hr>
            <h1>Register</h1>
            <hr>
        </div>
        <form>
            <v-text-field
                v-model="name"
                :error-messages="nameErrors"
                label="Name"
                required
                @input="$v.name.$touch()"
                @blur="$v.name.$touch()"
            ></v-text-field>
            <v-text-field
                v-model="address"
                :error-messages="addressErrors"
                label="Address"
                required
                @input="$v.address.$touch()"
                @blur="$v.address.$touch()"
            ></v-text-field>
            <v-text-field
                v-model="email"
                :error-messages="emailErrors"
                label="E-mail"
                required
                @input="$v.email.$touch()"
                @blur="$v.email.$touch()"
            ></v-text-field>
            <v-text-field
                v-model="password"
                :error-messages="passwordErrors"
                label="Password"
                required
                @input="$v.password.$touch()"
                @blur="$v.password.$touch()"
                type="password"
                autocomplete
            ></v-text-field>
            <v-text-field
                v-model="repeatpassword"
                :error-messages="repeatpasswordErrors"
                label="Repeat Password"
                required
                @input="$v.repeatpassword.$touch()"
                @blur="$v.repeatpassword.$touch()"
                type="password"
                autocomplete
            ></v-text-field>
            <div class="login-buttons">
                <v-btn class="submit-login" round color="accent" @click="submit">Register</v-btn>
            </div>
        </form>
    </div>
</template>
    
<script>
    import { validationMixin } from 'vuelidate'
    import { required, maxLength, email, sameAs } from 'vuelidate/lib/validators'
    import axios from 'axios'

    export default {
        name: 'register',
        mixins: [validationMixin],

        validations: {
            name: { required },
            address: { required },
            email: { required, email },
            password: { required },
            repeatpassword: {
                sameAsPassword: sameAs('password')
            }
        },

        data: () => ({
        name:'',
        address:'',
        email: '',
        password: '',
        repeatpassword: '',

        }),

        computed: {
            nameErrors () {
                const errors = []
                if (!this.$v.name.$dirty) return errors
                !this.$v.name.required && errors.push('Name is required')
                return errors
            },
            addressErrors () {
                const errors = []
                if (!this.$v.address.$dirty) return errors
                !this.$v.address.required && errors.push('Address is required')
                return errors
            },
            emailErrors () {
                const errors = []
                if (!this.$v.email.$dirty) return errors
                !this.$v.email.email && errors.push('Must be valid e-mail')
                !this.$v.email.required && errors.push('E-mail is required')
                return errors
            },
            passwordErrors () {
                const errors = []
                if (!this.$v.password.$dirty) return errors
                !this.$v.password.required && errors.push('Password is required')
                return errors
            },
            repeatpasswordErrors () {
                const errors = []
                if (!this.$v.repeatpassword.$dirty) return errors
                !this.$v.repeatpassword.sameAsPassword && errors.push('This field must match the password field')                
                return errors
            }
        },

        methods: {
            submit () {
                this.$v.$touch();

                
                axios.post('http://localhost:5000/api/users/register', {
                    name: this.name,
                    address:this.address,
                    email:this.email,
                    password:this.password,
                    repeatpassword:this.repeatpassword,
                })
                .then(function (response) {
                // handle success
                console.log(response);
                })
                .catch(function (error) {
                // handle error
                console.log(error);
                })
                
            },
    }
    }
</script>
    
<style>
.auth.register div.title-wrapper hr:first-child{
    width: 72%;
}
</style>