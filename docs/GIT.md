# Git + Github quickstarter for use with MeteorBeat.

## Requirements of usage (rules)

I'll list two requirements of MeteorBeat's Git usage then explain why we have them
and how to use Git while developing MeteorBeat.

Rules: 
1. Do not use private Git branches. 
2. Do not push non documentation related changes to master branch.

For rule #1, since our Git repository on Github is public it's not possible for
us to create private branches currently. Nothing to worry about there. Why are
private Git repositories bad? If one of us gets hit by a bus (or perhaps
something less tragic, we're visiting our grandparents that don't have an
internet connection). Everyone will still be able to access the code you've
produced so MeteorBeat's development is not held up.

For rule #2, this will ensure we'll at all times have a "safe" repository that
will have a run-able game. I.e. if bad code is committed it can be stopped in
code review and will not impact anyone else's productivity. 

## Start-up/setup

To setup all you need to do is clone the repository into an easily accessible
location on your machine.

Running `git clone https://github.com/mini-eggs/MeteorBeat` within your $HOME
folder (or wherever you prefer) is sufficient.

## Workflow

Rule #2 mentioned code review. It doesn't need to be a formal code review -- just
that a commit that includes code changes and/or additions to master branch is
vetted in some way. So what do the process of adding a feature to MeteorBeat
look like? I'll outline the steps below and provide the bash commands to
accomplish each in more details in the next step.

The high level is:
1. Create branch specific to your changes/additions.
2. Make the changes. The real work.
3. Push changes.
4. Submit a pull request and review changes.
5. Merge changes into master branch.

To make an addition or change to MeteorBeat, starting from checking out the
repository you will: 
1. Clone the repository: `git clone https://github.com/mini-eggs/MeteorBeat` if
   you've already clone'd it, run `git pull` to get the lastest code (be sure you run this
   while on the master branch `git status` to check and `git checkout master` to
   switch to it).
2. Navigate into the repository: `cd MeteorBeat`
3. Create a development branch for your feature or change and switch to the
   branch. Branches should follow the naming scheme `evan/feat/ship-particles`,
   that's `${name}/${type}/${short-description}`. `feat` is short for feature,
   `fix` would be for fixing bugs. To do the above, create a branch called
   `evan/feat/ship-particles` and switch to it, you would use the single
   command: `git checkout -b evan/feat/ship-particles`.
4. After you're on your branch you will make your code changes.
5. Once you are either (1) done with your code changes or (2) done with your
   current session (i.e. work is still in progress) you will make a commit and
   push it to the remote origin repository (which I will just call the
   repository from here on out, https://github.com/mini-eggs/MeteorBeat). To do
   this you would first add your code `git add ${files}`, I often run `git add
   .` to add all files I have modified, added, or deleted. Then create the
   commit. If it's a work in progress commit the command `git commit -m
   "WIP(particles): Still working on the particles color."` would suffice. You
   could also use Git's interactive commit style like: `git commit` which is
   what I usually do. Commonly this will bring up Nano or Vi (your $EDITOR) for you
   to enter your commit. Commits will follow the pattern: `Type(Subject): Description.`
6. Push your commit(s) to your branch. `git push`. If it's your first commit to
   the branch you will be asked to supply another command line argument like: 
   `git push --set-upstream origin ${branch-name}`.
7. Do steps #5 and #6 until your feature, fix, or whatever is complete.
8. Now it's time to get your code into the master branch. The easiest way to do
   this is to submit a pull request through the github.com UI. In a browser
   visit your branch on Github, `https://github.com/mini-eggs/MeteoBeat/tree/${branch-name}`.
   Click on `Compare & pull request`. Write some comments if you think it's
   necessary. Click `Create pull request`.
9. At this point you should get someone else to checkout your commit and they
   will merge it into master, but not every case needs that kind of attention.
   Simply, if the game still runs and you're not introducing obvious bugs or
   misspelled code, click `Merge pull request`.
10. At this point you can and should delete your branch. To delete your branch 
   (both remotely and localy) run: `git push --delete origin ${branch-name}` 
   then `git branch -d ${branch-name}`.


## Disaster recovery

Due to our `branch -> commits -> pull request -> master` style of creating
changes and additions to MeteorBeat, disaster recovery is made simple. If the
game is made severly defunct at any point in time rollback to the most recent previous
commit on the master branch of the repository. For code hosting safety it is
offloaded to Microsoft's hosting of Github. 

## Help

If you need any help or get yourself into a jam don't worry! Contact me (Evan)! If you
ran `git commit ...` on any of your code it will remain safe and recoverable.
