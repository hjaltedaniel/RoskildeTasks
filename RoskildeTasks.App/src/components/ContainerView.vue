<template>
    <div class="container is-fluid">
        <p>ALL TASKS</p>
        <div class="columns main-container">
            <div class="column is-three-fifths">
                <div class="columns">
                    <div class="column is-1">
                        <span class="notfication">+</span>
                    </div>
                    <div class="column is-11">
                        <p>Assignment</p>
                    </div>
                </div>
            </div>
            <div class="column">
                <p>Days</p>
            </div>
            <div class="column">
                <img src="../assets/Icons/comments--grey.svg" />
            </div>
            <div class="column">
                <p>Category</p>
            </div>
        </div>
    </div>
</template>

<style>
    .main-container:first-child {
        border-bottom: 1px solid red;
    }
    .main-container:last-child {
        border-top: 1px solid red;
    }
</style>

<script>
export default {
    
};
</script>