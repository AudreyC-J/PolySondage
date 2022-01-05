using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class TestDataBase
    {
        private readonly IPollRepository _pollRepo;
        private readonly IUserRepository _userRepo;
        private readonly IVoteRepository _voteRepo;
        private int user1 = 1;
        private int user2 = 2;
        private int user3 = 3;
        private int poll1 = 1;
        private int poll2 = 2;
        private int poll3 = 3;


        public TestDataBase(IPollRepository pollrepo, IUserRepository userrepo, IVoteRepository voterepo)
        {
            _pollRepo = pollrepo;
            _userRepo = userrepo;
            _voteRepo = voterepo;
        }

        public async Task test()
        {
            //await createdBaseExemple();
           // Debug.WriteLine("Base créer");
            if (await _pollRepo.GetNumberUserVotePollAsync(poll1) != 3)
                Debug.WriteLine("Mauvais nombre de user qui ont voté pour poll1");
            /*await testUser();
            Debug.WriteLine("Fin erreur Poll");
            await testPoll();
            Debug.WriteLine("Fin erreur User");
            await testVote();
            Debug.WriteLine("Fin erreur Vote");*/
        }

        private async Task createdBaseExemple()
        {
            /*User u1 = new User();
            u1.Email = "user1@mail.fr";
            u1.Password = "123";
            User u2 = new User();
            u2.Email = "user2@mail.fr";
            u2.Password = "456";
            User u3 = new User();
            u3.Email = "user3@mail.fr";
            u3.Password = "789";

            int id1 = await _userRepo.AddUserAsync(u1);
            int id2 = await _userRepo.AddUserAsync(u2);
            int id3 = await _userRepo.AddUserAsync(u3);
            Debug.WriteLine("id user 1 " + id1 + "\n id user 2 " + id2 + "\n id user 3 " + id3);
            user1 = id1;
            user2 = id2;
            user3 = id3;*/
            int id1 = user1;
            int id2 = user2;
            int id3 = user3;



            Choice tmp1 = new Choice();
            tmp1.Details = "c1";
            Choice tmp2 = new Choice();
            tmp2.Details = "c2";
            Choice tmp3 = new Choice();
            tmp3.Details = "c3";

            Poll p = new Poll();
            p.Unique = true;
            p.Choices.Add(tmp1);
            p.Choices.Add(tmp2);
            p.Choices.Add(tmp3);
            p.Title = "S1";
            //int s1 = await _pollRepo.AddPollAsync(p,id1);

            Choice tmp4 = new Choice();
            tmp4.Details = "c1";
            Choice tmp5 = new Choice();
            tmp5.Details = "c2";
            Choice tmp6 = new Choice();
            tmp6.Details = "c3";

            Poll p2 = new Poll();
            p2.Choices.Add(tmp4);
            p2.Choices.Add(tmp5);
            p2.Choices.Add(tmp6);
            p2.Unique = false;
            p2.Title = "S2";
            //int s2 = await _pollRepo.AddPollAsync(p2,id1);

            Choice tmp7 = new Choice();
            tmp7.Details = "c1";
            Choice tmp8 = new Choice();
            tmp8.Details = "c2";
            Choice tmp9 = new Choice();
            tmp9.Details = "c3";

            Poll p3 = new Poll();
            p3.Choices.Add(tmp7);
            p3.Choices.Add(tmp8);
            p3.Choices.Add(tmp9);
            p3.Unique = false;
            p3.Title = "S3";
            // int s3 = await _pollRepo.AddPollAsync(p3,id2);

            /*Debug.WriteLine("Sondage 1 " +s1+ " Sondage 2 " + s2 + " Sondage 3 " + s3);
            poll1 = s1;
            poll2 = s2;
            poll3 = s3;*/
            int s1 = poll1;
            int s2 = poll2;
            int s3 = poll3;


            List<Choice> v = new List<Choice>();
            v.Add(tmp1);
            await _voteRepo.AddVoteAsync(v,id1,s1);

            List<Choice> v1 = new List<Choice>();
            v1.Add(tmp1);
            await _voteRepo.AddVoteAsync(v1, id3, s1);

            List<Choice> v2 = new List<Choice>();
            v2.Add(tmp2);
            await _voteRepo.AddVoteAsync(v2, id2, s1);

            List<Choice> v3 = new List<Choice>();
            v3.Add(tmp4);
            v3.Add(tmp5);
            v3.Add(tmp6);
            await _voteRepo.AddVoteAsync(v3, id2, s2);

            List<Choice> v4 = new List<Choice>();
            v4.Add(tmp4);
            v4.Add(tmp5);
            await _voteRepo.AddVoteAsync(v4, id1, s2);
        }

        private async Task testUser()
        {
            User u1 = await _userRepo.getUserByIdAsync(user1);
            if (u1.Email != "user1@mail.fr" && u1.Password != "123")
                Debug.WriteLine("information de user1 non conforme");

            if (u1.PollsCreated.Count != 2)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u1.PollsCreated.Count);

            User u2 = await _userRepo.getUserByIdAsync(user2);
            if (u2.Email != "user2@mail.fr" && u2.Password != "456")
                Debug.WriteLine("information de user2 non conforme");

            if (u2.PollsCreated.Count != 1)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u2.PollsCreated.Count);

            User u3 = await _userRepo.getUserByIdAsync(user3);
            if (u3.Email != "user3@mail.fr" && u3.Password != "789")
                Debug.WriteLine("information de user3 non conforme");

            if (u3.PollsCreated.Count != 0)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u3.PollsCreated.Count);

            if (await _userRepo.connectUserAsync("user1@mail.fr", "123") != user1)
                Debug.WriteLine("Bon info de connexion mais mauvais retour");

            if (await _userRepo.connectUserAsync("user1@mail.fr", "124") != -1)
                Debug.WriteLine("Mauvaise mot de passe mais connexion accepté");

            if (await _userRepo.connectUserAsync("r1@mail.fr", "123") != -1)
                Debug.WriteLine("Mauvaise email mais connexion accepté");
        }

        private async Task testPoll()
        {
            Poll p2 = await _pollRepo.GetPollByIdAsync(poll2);
            if (p2.Creator.IdUser != user1 && p2.Choices[0].Details != "1")
                Debug.WriteLine("recherche par id renvoie pas le bon poll, id = " + p2.IdPoll + " et id user : " + p2.Creator.IdUser);

            if (!await _pollRepo.IsPollActivateAsync(poll3))
                Debug.WriteLine("le poll 3 doit être activé mais la fonction ne revoie pas cette information");
            await _pollRepo.DeactivatePollAsync(poll3);
            if (await _pollRepo.IsPollActivateAsync(poll3))
                Debug.WriteLine("le poll 3 doit être désactivé mais la fonction ne revoie pas cette information");

            if (await _pollRepo.GetNumberUserVotePollAsync(poll1) != 3)
                Debug.WriteLine("Mauvais nombre de user qui ont voté pour poll1");

            if (await _pollRepo.GetNumberUserVotePollAsync(poll2) != 2)
                Debug.WriteLine("Mauvais nombre de user qui ont voté pour poll2");
        }

        private async Task testVote()
        {
            Poll p = await _pollRepo.GetPollByIdAsync(poll2);
            List<Choice> Choices = new List<Choice>();
            Choices.Add(p.Choices[2]);
            Choices.Add(p.Choices[1]);

            int nbvote1 = p.Choices[0].TotalVotes;
            await _voteRepo.ChangeVoteAsync(Choices,user3,poll2);
            Poll pchange = await _pollRepo.GetPollByIdAsync(poll3);
            if (!(nbvote1 == pchange.Choices[0].TotalVotes + 1))
                Debug.WriteLine("le vote n'a pas été changé");
            if (pchange.Choices[0].TotalVotes == p.Choices[0].TotalVotes)
                Debug.WriteLine("mise à jour automatique du poll après changement de vote");

            List<Poll> plist = await _voteRepo.GetPaticipatedPollsByIdUserAsync(user1);
            if (plist.Count != 2)
                Debug.WriteLine("mauvaise lsite de participation d'un user");
        }
    }
}
